using Domain.Entities;

namespace Domain.Repositories.Abstractions;

public interface IUnitRepository
{
    Task<ICollection<Unit>> GetAllAsync(int? limit, int? offset);
    Task<int> CountAllAsync();
}
