using System;

namespace API.DTO
{
    public class CommentCreateDto
    {
        public string Body { get; set; }
        public Guid Id { get; set; }
    }
}