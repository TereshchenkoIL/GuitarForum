namespace Domain.Exceptions.CategoryExceptions
{
    public class CategoryUpdateException : BadRequestException
    {
        public CategoryUpdateException(string name) 
            : base($"Failed to update the ${name} category")
        {}
    }
}