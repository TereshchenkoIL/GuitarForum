using System;

namespace Contracts
{
    public class LikeDto
    {
        public string AppUserId { get; set; }
        
        public Guid TopicId { get; set; }
    }
}