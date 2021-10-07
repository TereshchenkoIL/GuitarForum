namespace Domain.Exceptions.ProfileExceptions
{
    public class ProfileUpdateException : BadRequestException
    {
        public ProfileUpdateException(string message) : base(message)
        {
        }
    }
}