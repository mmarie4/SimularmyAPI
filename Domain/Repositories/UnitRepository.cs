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
    ///     Get all units from db and refreshes cache
    /// </summary>
    /// <returns></returns>
    public async Task RefreshCache()
    {
        var units = await Entities.ToListAsync();
        UnitsStore.Update(units);
    }
}
