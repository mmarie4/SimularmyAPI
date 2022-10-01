using Domain.Entities;

namespace HeroicBrawlServer.DAL.Repositories.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
