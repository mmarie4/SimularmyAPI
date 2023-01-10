using Commands.ChangePassword;
using Commands.DeleteAccount;
using Commands.JoinMatchmakingQueue;
using Commands.RegisterPlayer;
using Domain.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queries.GetAccessToken;
using Queries.GetPlayerByEmail;
using SimularmyAPI.Extensions;
using SimularmyAPI.Models.Players;

namespace SimularmyAPI.Controllers;

[ApiController]
[Route("api/players")]
[ProducesErrorResponseType(typeof(DomainException))]
public class PlayerController : Controller
{
    private readonly IMediator _mediator;
    public PlayerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Checks if an account exists with an email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("exists/{email:required}")]
    [ProducesResponseType(typeof(ExistsResponse), 200)]
    public async Task<ExistsResponse> Exists([FromRoute] string email)
    {
        var user = await _mediator.Send(new GetPlayerByEmailQuery(email));

        return new ExistsResponse()
        {
            Exists = user is not null
        };
    }

    /// <summary>
    ///     Logs in an user
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(PlayerResponse), 200)]
    public async Task<PlayerResponse> Login([FromBody] LoginRequest loginRequest)
    {
        var (user, token) = await _mediator.Send(new GetAccessTokenQuery(loginRequest.Email, loginRequest.Password));

        var result = new PlayerResponse(user, token);
        return result;
    }

    /// <summary>
    ///     Register a new user
    /// </summary>
    /// <param name="signUpRequest"></param>
    /// <returns></returns>
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(PlayerResponse), 200)]
    public async Task<PlayerResponse> SignUp([FromBody] SignUpRequest signUpRequest)
    {
        signUpRequest.Validate();

        var (user, token) = await _mediator.Send(new RegisterPlayerCommand(signUpRequest.Pseudo,
                                                                           signUpRequest.Email,
                                                                           signUpRequest.Password));

        if (user is null)
            throw new DomainException(400, "Couldn't create user");

        var result = new PlayerResponse(user, token);
        return result;
    }

    /// <summary>
    ///     Updates user's password
    /// </summary>
    /// <param name="changePasswordRequest"></param>
    /// <returns></returns>
    [HttpPut("change-password")]
    [ProducesResponseType(204)]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        changePasswordRequest.Validate();

        var userId = HttpContext.User.ExtractUserId();

        await _mediator.Send(new ChangePasswordCommand(changePasswordRequest.OldPassword, changePasswordRequest.NewPassword, userId));

        return NoContent();
    }

    /// <summary>
    ///     Delete account
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(204)]
    [Authorize]
    public async Task<IActionResult> Delete()
    {
        var userId = HttpContext.User.ExtractUserId();

        await _mediator.Send(new DeleteAccountCommand(userId));

        return NoContent();
    }

    /// <summary>
    ///     Delete account
    /// </summary>
    /// <returns></returns>
    [HttpPost("join-matchmaking")]
    [ProducesResponseType(204)]
    [Authorize]
    public async Task<IActionResult> JoinMatchmaking([FromQuery] string connectionId)
    {
        var userId = HttpContext.User.ExtractUserId();

        await _mediator.Send(new JoinMatchmakingQueueCommand(userId, connectionId));

        return NoContent();
    }
}
