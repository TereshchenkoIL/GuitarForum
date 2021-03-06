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
       
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
       
        Task UpdateAsync(CategoryDto categoryForUpdation, CancellationToken cancellationToken = default);

        Task<CategoryDto> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}