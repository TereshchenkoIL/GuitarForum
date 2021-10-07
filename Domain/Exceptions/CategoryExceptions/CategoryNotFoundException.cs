using System;

namespace Domain.Exceptions.CategoryExceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(Guid categoryId) 
            : base($"The category with the identifier {categoryId} was not found.")
        {
        }
    }
}