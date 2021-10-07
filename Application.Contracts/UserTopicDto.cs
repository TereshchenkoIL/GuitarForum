using System;
using System.Text.Json.Serialization;

namespace Contracts
{
    public class UserTopicDto
    {
        public Guid TopicId { get; set; }

        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public string CreatorUsername  { get; set; }
    }
}