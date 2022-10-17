using Domain.Entities;
using Domain.Repositories.Abstractions;
using MediatR;

namespace Queries.GetArmy;

public class GetArmyQueryHandler : IRequestHandler<GetArmyQuery, Army>
{
    private readonly IArmyRepository _repository;

    public GetArmyQueryHandler(IArmyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Army> Handle(GetArmyQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.UserId) ?? new Army();
    }
}
