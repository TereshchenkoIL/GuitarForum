namespace Domain.Exceptions.TopicExceptions
{
    public class TopicUpdateException : BadRequestException
    {
        public TopicUpdateException(string message) : base(message)
        {
        }
    }
}