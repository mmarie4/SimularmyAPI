using Domain.Infrastructure;
using Domain.Repositories.Abstractions;
using MediatR;
using Unit = MediatR.Unit;

namespace Commands.UpdateArmy;

public class UpdateArmyCommandHandler : IRequestHandler<UpdateArmyCommand>
{
    private readonly IUserRepository _repository;
    public UpdateArmyCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateArmyCommand request, CancellationToken cancellationToken)
    {

        var user = await _repository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new DomainException(404, $"User not found with id {request.UserId}");

        // TODO: Validate the amount of cash spent based on the price of each unit

        user.Units = request.ArmyUnits;

        await _repository.Update(user, request.UserId);
        await _repository.SaveAsync();

        return Unit.Value;
    }
}
