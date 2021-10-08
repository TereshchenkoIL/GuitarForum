using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class CommentUpdateDto
    {
        public Guid  Id { get; set; }
        
        public string Body { get; set; } 
    }
}