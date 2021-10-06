﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Body { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public Category Category { get; set; }
        
        public AppUser Creator { get; set; }
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}