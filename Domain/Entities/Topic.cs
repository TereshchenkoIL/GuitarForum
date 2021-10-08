using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public Category Category { get; set; }
        
        public AppUser Creator { get; set; }
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}