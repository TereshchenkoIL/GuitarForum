namespace Domain.Exceptions.TopicExceptions
{
    public class TopicDeleteException : BadRequestException
    {
        public TopicDeleteException(string message) : base(message)
        {
        }
    }
}