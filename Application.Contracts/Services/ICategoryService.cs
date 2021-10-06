using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Contracts.Services
{
    public interface ICategoryService : IService<CategoryDto>
    {
        Task<Category> GetByIdAsync(Guid categoryId, bool trackChanges, CancellationToken cancellationToken = default);
        
    }
}