using Domain.Entities;

namespace SimularmyAPI.Models.Units;

public class UnitResponse
{
    public int Id { get; }
    public string Name { get; }
    public UnitStatsResponse Stats { get; }

    public UnitResponse(Unit unit)
    {
        Id = unit.Id;
        Name = unit.Name;
        Stats = new UnitStatsResponse(unit.Stats);
    }
}
