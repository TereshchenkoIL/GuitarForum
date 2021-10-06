using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Contracts.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoDto> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}