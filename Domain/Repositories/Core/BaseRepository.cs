using Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Repositories.Core
{
    public class BaseRepository : IBaseRepository
    {
        public BaseRepository([NotNull] DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected DbContext Context { get; }

        /// <summary>
        ///     Saves changes
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}