using System;

namespace Contracts
{
    public class UserTopicDto
    {
        public Guid TopicId { get; set; }

        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}