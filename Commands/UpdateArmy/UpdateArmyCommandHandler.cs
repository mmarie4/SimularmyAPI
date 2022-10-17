using Domain.Entities;
using Domain.Repositories.Abstractions;
using MediatR;
using Unit = MediatR.Unit;

namespace Commands.UpdateArmy;

public class UpdateArmyCommandHandler : IRequestHandler<UpdateArmyCommand>
{
    private readonly IArmyRepository _repository;
    public UpdateArmyCommandHandler(IArmyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateArmyCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate the amount of cash spent based on the price of each unit

        var army = await _repository.GetByIdAsync(request.UserId);

        if (army == null)
        {
            army = new Army()
            {
                Id = request.UserId,
                UserUnits = request.ArmyUnits
            };
            army.Init(request.UserId);

            await _repository.AddAsync(army);
            await _repository.SaveAsync();

            return Unit.Value;
        }

        army.UserUnits = request.ArmyUnits;
        army.Update(request.UserId);

        await _repository.Update(army);
        await _repository.SaveAsync();

        return Unit.Value;
    }
}
