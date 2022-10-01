using Domain.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await Entities
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
