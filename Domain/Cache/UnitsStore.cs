using Domain.Entities;

namespace Domain.Cache;

public static class UnitsStore
{
    private static IDictionary<int, Unit> Units { get; set; } = new Dictionary<int, Unit>();

    public static void Update(ICollection<Unit> units)
    {
        foreach (var unit in units)
        {
            Units[unit.Id] = unit;
        }
    }

    public static Unit GetById(int id)
    {
        return Units[id];
    }

    public static ICollection<Unit> GetAll(int? limit = null, int? offset = null)
    {
        if (limit.HasValue)
        {
            return Units.Values.Skip(offset ?? 0)
                               .Take(limit.Value)
                               .ToList();
        }
        return Units.Values;
    }
}
