using Domain.Cache;
using Domain.Entities;
using Domain.Repositories.Abstractions;
using Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Repositories;

public class UnitRepository : BaseRepository, IUnitRepository
{
    private readonly ILogger<UnitRepository> _logger;

    public UnitRepository(AppDbContext context, ILogger<UnitRepository> logger)
        : base(context)
    {
        _logger = logger;
    }

    protected DbSet<Unit> Entities => Context.Set<Unit>();

    /// <summary>
    ///     Get all units from db and refreshes cache
    /// </summary>
    /// <returns></returns>
    public async Task RefreshCache()
    {
        var units = await Entities.ToListAsync();
        UnitsStore.Update(units);

        _logger.LogInformation("{count} loaded in units cache", units.Count);
    }
}
