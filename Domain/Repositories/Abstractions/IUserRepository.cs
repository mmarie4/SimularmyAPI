using Domain.Entities;

namespace Domain.Repositories.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
