using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category
    {
        
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        
    }
}