using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queries.GetAllUnits;
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

    [HttpGet]
    public async Task<UnitCollectionResponse> GetAllAsync([FromQuery] int? limit, [FromQuery] int? offset)
    {
        var (units, totalCount) = await _mediator.Send(new GetAllUnitsQuery(limit, offset));

        var result = new UnitCollectionResponse(units, limit, offset, totalCount);
        return result;
    }
}
