using Commands.UpdateArmy;
using Domain.Entities;
using Domain.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queries.GetArmy;
using SimularmyAPI.Extensions;
using SimularmyAPI.Models.Army;

namespace SimularmyAPI.Controllers;

[ApiController]
[Route("api/army")]
[ProducesErrorResponseType(typeof(DomainException))]
[Authorize]
public class ArmyController : Controller
{
    private readonly IMediator _mediator;

    public ArmyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get player's army
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ArmyResponse> GetArmy()
    {
        var userId = HttpContext.User.ExtractUserId();
        var army = await _mediator.Send(new GetArmyQuery(userId));

        return new ArmyResponse(army);
    }

    /// <summary>
    ///     Updates player's army
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateArmy([FromBody] ICollection<UserUnit> userUnits)
    {
        var userId = HttpContext.User.ExtractUserId();
        await _mediator.Send(new UpdateArmyCommand(userUnits, userId));

        return Ok();
    }
}
