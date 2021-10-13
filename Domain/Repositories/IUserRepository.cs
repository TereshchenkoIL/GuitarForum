using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<AppUser>
    {
        Task<AppUser> GetByUserIdAsync(string userId,  CancellationToken cancellationToken);
        
        Task<AppUser> GetByUsernameAsync(string username,  CancellationToken cancellationToken);
        
        
    }
}