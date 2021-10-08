using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<AppUser>
    {
        Task<AppUser> GetByUserId(string userId,  CancellationToken cancellationToken);
        
        Task<AppUser> GetByUsername(string username,  CancellationToken cancellationToken);
    }
}