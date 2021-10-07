using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface ICategoryService 
    {
        Task<CategoryDto> GetByIdAsync(Guid categoryId,  CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryDto>> GetAllAsync( CancellationToken cancellationToken = default);

        Task CreateAsync(CategoryDto categoryForCreation, CancellationToken cancellationToken = default);
       
        Task DeleteAsync(CategoryDto categoryForDeletion, CancellationToken cancellationToken = default);
       
        Task UpdateAsync(CategoryDto categoryForUpdation, CancellationToken cancellationToken = default);
    }
}