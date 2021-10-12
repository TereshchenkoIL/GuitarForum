using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Category
    {
        
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Topic> Topics { get; set; }
    }
}