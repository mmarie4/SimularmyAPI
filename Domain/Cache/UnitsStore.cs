using Domain.Entities;

namespace Domain.Cache;

public static class UnitsStore
{
    private static IDictionary<int, Unit> _units { get; set; } = new Dictionary<int, Unit>();

    public static void Update(ICollection<Unit> units)
    {
        foreach (var unit in units)
        {
            _units[unit.Id] = unit;
        }
    }

    public static Unit GetById(int id)
    {
        return _units[id];
    }

    public static ICollection<Unit> GetAll(int? limit = null, int? offset = null)
    {
        if (limit.HasValue)
        {
            return _units.Values.Skip(offset ?? 0)
                               .Take(limit.Value)
                               .ToList();
        }
        return _units.Values;
    }

    public static int Count => _units.Count;
}
