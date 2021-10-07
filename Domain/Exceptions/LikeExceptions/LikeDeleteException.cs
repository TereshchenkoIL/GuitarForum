namespace Domain.Exceptions.LikeExceptions
{
    public class LikeDeleteException : BadRequestException
    {
        public LikeDeleteException(string message) : base(message)
        {
        }
    }
}