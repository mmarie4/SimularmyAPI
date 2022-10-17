using Domain.Entities;
using Domain.Repositories.Abstractions;
using Domain.Repositories.Core;

namespace Domain.Repositories
{
    public class ArmyRepository : EntityRepository<Army>, IArmyRepository
    {
        public ArmyRepository(AppDbContext context) : base(context) { }
    }
}
