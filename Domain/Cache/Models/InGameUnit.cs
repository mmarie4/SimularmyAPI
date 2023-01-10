using Domain.Entities;
using Domain.Enums;

namespace Domain.Cache.Models;

public class InGameUnit
{
    public Guid Id { get; set; }
    public int UnitId { get; set; }
    public double PosX { get; set; }
    public double PosY { get; set; }
    public int Hp { get; set; }
    public InGameUnitState State { get; set; }
    public Guid Target { get; set; }
    public double SinAlpha { get; set; }
    public double CosAlpha { get; set; }

    public void TakeDamage(int damages)
    {
        Hp -= damages;

        if (Hp <= 0)
            State = InGameUnitState.Dead;
    }

    public bool IsAlive => State != InGameUnitState.Dead;
    public bool IsTank => UnitsStore.GetById(UnitId).Type == UnitType.Tank;


    public static IEnumerable<InGameUnit> InitFromUserArmy(IEnumerable<UserUnit> userUnits, double playerPosX)
    {
        var inGameUnits = new List<InGameUnit>();
        
        foreach (var userUnit in userUnits)
        {
            var unit = UnitsStore.GetById(userUnit.UnitId);

            var posX = playerPosX == 0
                ? playerPosX + userUnit.Offset
                : playerPosX - userUnit.Offset;

            // TODO: calculate based on number of units to split units on screen height
            var posY = 0;

            inGameUnits.Add(new InGameUnit()
            {
                Id = Guid.NewGuid(),
                Hp = unit.Stats.Health,
                UnitId = unit.Id,
                PosX = posX,
                PosY = posY,
                State = InGameUnitState.Idle
            });
        }

        return inGameUnits;
    }
}
