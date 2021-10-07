namespace Domain.Exceptions.PhotoExceptions
{
    public class PhotoCreateException : BadRequestException
    {
        public PhotoCreateException(string message) : base(message)
        {
        }
    }
}