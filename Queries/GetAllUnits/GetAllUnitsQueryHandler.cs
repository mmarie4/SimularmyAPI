using Domain.Repositories.Abstractions;
using MediatR;

namespace Queries.GetAllUnits;

public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, (ICollection<Domain.Entities.Unit>, int)>
{
    private readonly IUnitRepository _unitRepository;

    public GetAllUnitsQueryHandler(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<(ICollection<Domain.Entities.Unit>, int)> Handle(GetAllUnitsQuery request,
                                                                CancellationToken cancellationToken)
    {
        var units = await _unitRepository.GetAllAsync(request.Limit, request.Offset);
        var totalCount = await _unitRepository.CountAllAsync();

        return (units, totalCount);
    }
}
