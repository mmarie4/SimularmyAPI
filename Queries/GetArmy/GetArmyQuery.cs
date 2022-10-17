using Domain.Entities;
using MediatR;

namespace Queries.GetArmy;

public class GetArmyQuery : IRequest<Army>
{
    public Guid UserId { get; }

    public GetArmyQuery(Guid userId)
    {
        UserId = userId;
    }
}
