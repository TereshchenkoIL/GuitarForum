namespace Domain.Exceptions.CommentExceptions
{
    public class CommentCreateException : BadRequestException
    {
        public CommentCreateException(string message) : base(message)
        {
        }
    }
}