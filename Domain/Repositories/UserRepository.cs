using Domain.Entities;
using Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await Entities
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
