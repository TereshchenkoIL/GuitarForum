using System.Collections.Generic;

namespace Domain.Entities
{
    public class AppUser
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }
        
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}