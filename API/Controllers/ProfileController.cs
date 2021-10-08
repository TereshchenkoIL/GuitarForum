using System.Threading.Tasks;
using API.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfileController : BaseApiController
    {
        public ProfileController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        [HttpGet("username")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var profile = await ServiceManager.ProfileService.GetDetails(username);

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserUpdateDto userUpdateDto)
        {
            await ServiceManager.ProfileService.UpdateAsync(userUpdateDto.DisplayName, userUpdateDto.Bio);

            return Ok();
        }

        [HttpGet("{id}/topics")]
        public async Task<IActionResult> GetTopics(string id)
        {
            var topics = await ServiceManager.TopicService.GetAllByCreatorIdAsync(id);

            return Ok(topics);
        }
    }
}