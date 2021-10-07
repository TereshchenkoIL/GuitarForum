namespace Domain.Exceptions.LikeExceptions
{
    public class LikeCreateException : BadRequestException
    {
        public LikeCreateException(string message) : base(message)
        {
        }
    }
}