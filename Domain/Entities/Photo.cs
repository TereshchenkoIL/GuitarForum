using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Photo
    {
        public string Id { get; set; }
        [Required]
        public string Url { get; set; }

        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }
        
    }
}