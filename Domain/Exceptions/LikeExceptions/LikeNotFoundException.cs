using System;

namespace Domain.Exceptions.LikeExceptions
{
    public class LikeNotFoundException : NotFoundException
    {
        public LikeNotFoundException(string displayName, string topicTtitle) 
            : base($"The ${topicTtitle} topic doesn't have like from ${displayName}")
        {
        }
        public LikeNotFoundException(string message) 
            : base(message)
        {
        }
    }
}