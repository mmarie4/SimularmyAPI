using Domain.Entities;
using MediatR;

namespace Commands.UpdateArmy;

public class UpdateArmyCommand : IRequest
{
    public Guid UserId { get; set; }
    public ICollection<UserUnit> ArmyUnits { get; set; } = new List<UserUnit>();

    public UpdateArmyCommand(ICollection<UserUnit> armyUnits, Guid userId)
    {
        ArmyUnits = armyUnits;
        UserId = userId;
    }
}
