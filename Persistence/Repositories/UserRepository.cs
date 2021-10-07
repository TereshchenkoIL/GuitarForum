using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository : RepositoryBase<AppUser>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<AppUser> GetByUserId(string userId, bool trackChanges, CancellationToken cancellationToken)
        {
            return !trackChanges
                ? await Context.Users
                    .Include(p => p.Photo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken)
                : await Context.Users
                    .Include(p => p.Photo)
                    .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken);
        }

        public async Task<AppUser> GetByUsername(string username, bool trackChanges, CancellationToken cancellationToken)
        {
            return !trackChanges
                ? await Context.Users
                    .Include(p => p.Photo)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken: cancellationToken)
                : await Context.Users
                    .Include(p => p.Photo)
                    .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken: cancellationToken);
        }
    }
}