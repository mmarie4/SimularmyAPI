using Commands.UpdateArmy;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queries.GetArmy;
using SimularmyAPI.Extensions;

namespace SimularmyAPI.Controllers;

[ApiController]
[Route("api/army")]
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
    public async Task<Army> GetArmy()
    {
        var userId = HttpContext.User.ExtractUserId();
        var army = await _mediator.Send(new GetArmyQuery(userId));

        return army;
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
