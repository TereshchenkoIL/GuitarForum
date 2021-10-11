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

        public async Task<AppUser> GetByUserId(string userId,  CancellationToken cancellationToken)
        {
            return await Context.Users
                    .Include(p => p.Photo)
                    .Include(t => t.Topics)
                    .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken);
        }

        public async Task<AppUser> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return  await Context.Users
                    .Include(p => p.Photo)
                    .Include(t => t.Topics)
                    .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken: cancellationToken);
        }
    }
}