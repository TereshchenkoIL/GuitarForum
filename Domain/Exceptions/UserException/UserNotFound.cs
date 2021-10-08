namespace Domain.Exceptions.UserException
{
    public class UserNotFound : NotFoundException
    {
        public UserNotFound(string username) 
            : base($"User  {username} not found")
        {
        }
        
        
        
    }
}