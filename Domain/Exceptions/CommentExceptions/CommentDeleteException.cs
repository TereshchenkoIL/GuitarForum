namespace Domain.Exceptions.CommentExceptions
{
    public class CommentDeleteException : BadRequestException
    {
        public CommentDeleteException(string message) : base(message)
        {
        }
    }
}