using Domain.Cache;
using Domain.Entities;
using Domain.Repositories.Abstractions;
using Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class UnitRepository : BaseRepository, IUnitRepository
{
    public UnitRepository(AppDbContext context) : base(context) { }

    protected DbSet<Unit> Entities => Context.Set<Unit>();

    /// <summary>
    ///     Get all entities in this repository
    /// </summary>
    /// <param name="limit">Max number of entities</param>
    /// <param name="offset">First index of entity</param>
    /// <returns>Collection of entities</returns>
    public async Task<ICollection<Unit>> GetAllAsync(int? limit, int? offset)
    {
        var units = UnitsStore.GetAll(limit, offset);
        if (!units.Any())
        {
            if (limit.HasValue)
            {
                units = await Entities
                                .Skip(offset ?? 0)
                                .Take(limit.Value)
                                .ToListAsync();
            }
            else
            {
                units = await Entities.ToListAsync();
            }
            UnitsStore.Update(units);
            return units;
        }

        return units;
    }

    /// <summary>
    ///     Count all entities in this repository
    /// </summary>
    /// <param name="limit">Max number of entities</param>
    /// <param name="offset">First index of entity</param>
    /// <returns>Collection of entities</returns>
    public async Task<int> CountAllAsync()
    {
        var units = UnitsStore.GetAll();
        if (!units.Any())
        {
            return await Entities.CountAsync();
        }

        return units.Count;
    }
}
