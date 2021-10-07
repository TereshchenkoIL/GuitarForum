namespace Domain.Exceptions.TopicExceptions
{
    public class TopicCreateException : BadRequestException
    {
        public TopicCreateException(string message) : base(message)
        {
        }
    }
}