using System.Threading.Tasks;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class PhotosController : BaseApiController
    {
        public PhotosController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] IFormFile file)
        {
            var photo = await ServiceManager.PhotoService.CreateAsync(file);

            return Ok(photo);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await ServiceManager.PhotoService.DeleteAsync(id);
            return Ok();
        }
    }
}