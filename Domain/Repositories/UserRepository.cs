using Domain.Entities;
using Domain.Repositories.Abstractions;
using Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class UserRepository : EntityRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
            => await Entities.FirstOrDefaultAsync(x => x.Email == email);
    }
}
