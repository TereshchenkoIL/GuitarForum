namespace Domain.Exceptions.CommentExceptions
{
    public class CommentUpdateException : BadRequestException
    {
        public CommentUpdateException(string message) : base(message)
        {
        }
    }
}