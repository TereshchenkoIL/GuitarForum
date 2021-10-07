namespace Domain.Exceptions.CategoryExceptions
{
    public class CategoryDeleteException : BadRequestException
    {
        public CategoryDeleteException(string name) 
        : base($"Failed to delete the ${name} category")
        {
        }
    }
}