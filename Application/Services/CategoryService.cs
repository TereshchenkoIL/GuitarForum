using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Contracts.Services;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CategoryDto> GetByIdAsync(Guid categoryId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDto>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(CategoryDto categoryForCreation, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CategoryDto categoryForDeletion, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(CategoryDto categoryForUpdation, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}