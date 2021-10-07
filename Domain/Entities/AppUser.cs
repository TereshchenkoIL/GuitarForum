using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }
        
        public DateTime JoinData { get; set; }
        
        public Photo Photo { get; set; }
        
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}