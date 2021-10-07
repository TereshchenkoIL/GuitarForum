using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Contracts.Services
{
    public interface ICategoryService 
    {
        Task<Category> GetByIdAsync(Guid categoryId, bool trackChanges, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryDto>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default);

        Task CreateAsync(CategoryDto categoryForCreation, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(CategoryDto categoryForDeletion, CancellationToken cancellationToken = default);
       
        Task UpdateAsync(CategoryDto categoryForUpdation, CancellationToken cancellationToken = default);
    }
}