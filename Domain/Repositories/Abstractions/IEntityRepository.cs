namespace Domain.Repositories.Abstractions;

public interface IEntityRepository<TEntity> : IBaseRepository
{
    Task<ICollection<TEntity>> GetAllAsync(int? limit, int? offset);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity> AddAsync(TEntity entity, Guid userIdCaller);
    Task<TEntity> Update(TEntity entity, Guid userIdCaller);
    Task<TEntity> Remove(TEntity entity);
}
