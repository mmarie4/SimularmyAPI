using Domain.Entities;

namespace SimularmyAPI.Models.Units;

public class UnitStatsResponse
{
    public int Damages { get; }
    public int Health { get; }
    public double Speed { get; }
    public double Size { get; }
    public double AttackRange { get; }

    public UnitStatsResponse(UnitStats stats)
    {
        Damages = stats.Damages;
        Health = stats.Health;
        Speed = stats.Speed;
        Size = stats.Size;
        AttackRange = stats.AttackRange;
    }
}
