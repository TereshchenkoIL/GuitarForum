using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<AppUser>
    {
        Task<AppUser> GetByUserId(string userId, bool trackChanges, CancellationToken cancellationToken);
        
        Task<AppUser> GetByUsername(string username, bool trackChanges, CancellationToken cancellationToken);
    }
}