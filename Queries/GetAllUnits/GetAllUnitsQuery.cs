using MediatR;

namespace Queries.GetAllUnits;

public class GetAllUnitsQuery : IRequest<(ICollection<Domain.Entities.Unit>, int)>
{
    public GetAllUnitsQuery(int? limit, int? offset)
    {
        Limit = limit;
        Offset = offset;
    }

    public int? Limit { get; }
    public int? Offset { get; }

    
}
