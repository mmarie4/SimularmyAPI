﻿using Commands.RefreshUnitsCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queries.GetAllUnits;
using SimularmyAPI.Extensions;
using SimularmyAPI.Models.Units;

namespace SimularmyAPI.Controllers;

[ApiController]
[Route("api/units")]
public class UnitController : Controller
{
    private readonly IMediator _mediator;

    public UnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Returns paginated list of units library
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<UnitCollectionResponse> GetAllAsync([FromQuery] int? limit, [FromQuery] int? offset)
    {
        var (units, totalCount) = await _mediator.Send(new GetAllUnitsQuery(limit, offset));

        var result = new UnitCollectionResponse(units, limit, offset, totalCount);
        return result;
    }

    /// <summary>
    ///     Refreshes cache using units in db
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh")]
    public async Task RefreshCache()
    {
        var userId = HttpContext.User.ExtractUserId();
        await _mediator.Send(new RefreshUnitsCacheCommand(userId));
    }
}
