namespace Domain.Exceptions.CategoryExceptions
{
    public class CategoryCreateException : BadRequestException
    {
        public CategoryCreateException(string name) 
            : base($"Failed to create the ${name} category")
        {
        }
    }
}