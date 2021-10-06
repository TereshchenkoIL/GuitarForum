using System;

namespace Domain.Entities
{
    public class Like
    {
        public string AppUserId { get; set; }
        
        public AppUser AppUser { get; set; }
        
        public Guid TopicId { get; set; }
        
        public Topic Topic { get; set; }

    }
}