using Domain.Entities;
using Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Repositories.Core;

public class EntityRepository<TEntity> : BaseRepository, IEntityRepository<TEntity>
    where TEntity : BaseEntity
{
    public EntityRepository([NotNull] DbContext context) : base(context) { }

    protected DbSet<TEntity> Entities => Context.Set<TEntity>();

    /// <summary>
    ///     Get all entities in this repository
    /// </summary>
    /// <param name="limit">Max number of entities</param>
    /// <param name="offset">First index of entity</param>
    /// <returns>Collection of entities</returns>
    public async Task<ICollection<TEntity>> GetAllAsync(int? limit, int? offset)
    {
        if (limit.HasValue)
        {
            offset ??= 0;
            return await Entities
                            .Skip(offset.Value)
                            .Take(limit.Value)
                            .ToListAsync();
        }

        return await Entities
            .ToListAsync();
    }

    /// <summary>
    ///     Get one entity by id
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <returns></returns>
    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    ///     Adds an entity in the repository
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>The entity added</returns>
    public async Task<TEntity> AddAsync(TEntity entity, Guid userIdCaller)
    {
        entity.CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        entity.CreatedById = userIdCaller;
        entity.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        entity.UpdatedById = userIdCaller;
        var result = await Entities.AddAsync(entity);
        return result.Entity;
    }

    /// <summary>
    ///     Updates an entity in the repository
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The updated entity</returns>
    public Task<TEntity> Update(TEntity entity, Guid userIdCaller)
    {
        entity.CreatedAt = DateTime.SpecifyKind(entity.CreatedAt, DateTimeKind.Utc);
        entity.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        entity.UpdatedById = userIdCaller;
        var result = Entities.Update(entity);
        return Task.FromResult(result.Entity);
    }

    /// <summary>
    ///     Removes an entity from the repository
    /// </summary>
    /// <param name="entity">The entity to remove</param>
    /// <returns>The removed entity</returns>
    public Task<TEntity> Remove(TEntity entity)
    {
        var result = Entities.Remove(entity);
        return Task.FromResult(result.Entity);
    }
}
