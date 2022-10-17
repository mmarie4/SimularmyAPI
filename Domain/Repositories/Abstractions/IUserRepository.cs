using Domain.Entities;

namespace Domain.Repositories.Abstractions;

public interface IUserRepository : IEntityRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
