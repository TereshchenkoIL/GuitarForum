using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Contracts
{
    public class TopicDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Body { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public bool IsLiked { get; set; }
        public int Likes { get; set; }
        
        public Category Category { get; set; }
        
        public Profile Creator { get; set; }
        
    }
}