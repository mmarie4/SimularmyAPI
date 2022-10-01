using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        public BaseRepository([NotNull] DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected DbContext Context { get; }
        protected DbSet<TEntity> Entities => Context.Set<TEntity>();

        /// <summary>
        /// Saves changes
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all entities in this repository
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
        /// Get one entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Adds an entity in the repository
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>The entity added</returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await Entities.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The updated entity</returns>
        public Task<TEntity> Update(TEntity entity)
        {
            var result = Entities.Update(entity);
            return Task.FromResult(result.Entity);
        }

        /// <summary>
        /// Removes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        /// <returns>The removed entity</returns>
        public Task<TEntity> Remove(TEntity entity)
        {
            var result = Entities.Remove(entity);
            return Task.FromResult(result.Entity);
        }
    }
}