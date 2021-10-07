using System;

namespace Domain.Exceptions.TopicExceptions
{
    public class TopicNotFoundException : NotFoundException
    {
        public TopicNotFoundException(Guid topicId) 
            : base($"The topic with the identifier {topicId} was not found.")
        {
        }
    }
}