namespace Domain.Exceptions.PhotoExceptions
{
    public class PhotoDeleteException : BadRequestException
    {
        public PhotoDeleteException(string message) : base(message)
        {
        }
    }
}