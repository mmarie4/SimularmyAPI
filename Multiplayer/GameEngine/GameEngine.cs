using Domain.Cache;
using Domain.Cache.Models;
using Domain.Enums;
using Multiplayer.GameEngine.Models;

namespace Domain.GameEngine;

public class GameEngine : IGameEngine
{
    public void Update(Room room)
    {
        UpdatePlayerState(room.PlayerA, room.PlayerB);
        UpdatePlayerState(room.PlayerB, room.PlayerA);
    }

    public Guid? GetWinner(Room room)
    {
        if (room.PlayerA.Units.Count() > room.PlayerB.Units.Count())
            return room.PlayerA.Id;

        if (room.PlayerB.Units.Count() < room.PlayerB.Units.Count())
            return room.PlayerB.Id;

        return null;
    }

    private void UpdatePlayerState(PlayerState playerState, PlayerState opponent)
    {
        foreach (var userUnit in playerState.Units)
        {
            var target = FindTarget(userUnit, opponent.Units);

            userUnit.Target = target.Id;
            userUnit.CosAlpha = (target.PosX - userUnit.PosX) / target.Distance;
            userUnit.SinAlpha = (target.PosY - userUnit.PosY) / target.Distance;

            var unit = UnitsStore.GetById(userUnit.UnitId);

            if (unit.Stats.AttackRange <= target.Distance)
            {
                userUnit.State = InGameUnitState.Attacking;
                opponent.Units.First(u => u.Id == userUnit.Target).TakeDamage(unit.Stats.Damages);
            }
            else
            {
                userUnit.State = InGameUnitState.Moving;
                userUnit.PosX = unit.Stats.Speed * userUnit.CosAlpha;
                userUnit.PosY = unit.Stats.Speed * userUnit.SinAlpha;
            }
        }
    }

    private Target FindTarget(InGameUnit userUnit, IEnumerable<InGameUnit> opponentUnits)
    {
        var tanks = opponentUnits.Where(u => u.IsAlive && u.IsTank);
        if (tanks.Any())
            return tanks.Select(u => new Target(u.Id, Distance(u.PosX, userUnit.PosX, u.PosY, userUnit.PosY), u.PosX, u.PosY))
                        .OrderBy(t => t.Distance)
                        .First();

        return opponentUnits.Select(u => new Target(u.Id, Distance(u.PosX, userUnit.PosX, u.PosY, userUnit.PosY), u.PosX, u.PosY))
                            .OrderBy(t => t.Distance)
                            .First();
    }

    private double Distance(double x1, double x2, double y1, double y2)
        => Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
}
