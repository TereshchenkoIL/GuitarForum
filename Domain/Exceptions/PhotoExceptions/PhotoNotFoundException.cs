namespace Domain.Exceptions.PhotoExceptions
{
    public class PhotoNotFoundException : NotFoundException
    {
        public PhotoNotFoundException(string photoId) 
            : base($"Photo with id ${photoId} not found")
        {
        }
    }
}