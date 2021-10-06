using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<Category> GetByIdAsync(Guid categoryId, bool trackChanges, CancellationToken cancellationToken = default);
    }
}