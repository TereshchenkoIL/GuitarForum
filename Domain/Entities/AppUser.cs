using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }
        [Required]
        public string Bio { get; set; }

        [Required] public DateTime JoinData { get; set; } = DateTime.UtcNow;
        
        public Photo Photo { get; set; }
        
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Like> Likes { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}