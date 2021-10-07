namespace Domain.Exceptions.UserException
{
    public class UserNotFound : NotFoundException
    {
        public UserNotFound(string userId) 
            : base($"User  {userId} not found")
        {
        }
    }
}