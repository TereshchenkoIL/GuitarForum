using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.CategoryExceptions;
using Domain.Repositories;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetByIdAsync(Guid categoryId,  CancellationToken cancellationToken = default)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId,  cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryId);
            }

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync( CancellationToken cancellationToken = default)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task CreateAsync(CategoryDto categoryForCreation, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(categoryForCreation);
            _unitOfWork.CategoryRepository.Create(category);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (!result) throw new CategoryCreateException(categoryForCreation.Name);
        }

        public async Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId, cancellationToken);

            if (category == null) throw new CategoryNotFoundException(categoryId);
           _unitOfWork.CategoryRepository.Delete(category);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (!result) throw new CategoryDeleteException(category.Name);
        }

        public async Task UpdateAsync(CategoryDto categoryForUpdation, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(categoryForUpdation));

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (!result) throw new CategoryUpdateException(categoryForUpdation.Name);
        }
    }
}