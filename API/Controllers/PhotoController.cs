using System.Threading.Tasks;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class PhotoController : BaseApiController
    {
        public PhotoController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] IFormFile file)
        {
            await ServiceManager.PhotoService.CreateAsync(file);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await ServiceManager.PhotoService.DeleteAsync(id);
            return Ok();
        }
    }
}