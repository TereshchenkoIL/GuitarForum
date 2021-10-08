using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comment
    {
        public Guid  Id { get; set; }

        [Required]
        public string Body { get; set; } 
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       
        public AppUser Author { get; set; }
        
        public Topic Topic { get; set; }
        
     
    }
}