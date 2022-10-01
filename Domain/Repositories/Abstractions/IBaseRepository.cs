using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroicBrawlServer.DAL.Repositories.Abstractions
{
    public interface IBaseRepository<TEntity>
    {
        Task<int> SaveAsync();
        Task<ICollection<TEntity>> GetAllAsync(int? limit, int? offset);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Remove(TEntity entity);
    }
}
