namespace Domain.Exceptions
{
    public class CloudinaryException : BadRequestException
    {
        public CloudinaryException() : base("Problem deleting photo from Cloudinary")
        {
        }
    }
}