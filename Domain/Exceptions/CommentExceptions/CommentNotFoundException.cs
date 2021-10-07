namespace Domain.Exceptions.CommentExceptions
{
    public class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException(string displayName, string topicTitle) 
            : base($"Comment from ${displayName} about {topicTitle} not found ")
        {
        }
        public CommentNotFoundException(string displayName) 
            : base($"Comment from ${displayName}")
        {
        }
    }
}