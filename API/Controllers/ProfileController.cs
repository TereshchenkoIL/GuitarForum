using System.Threading.Tasks;
using API.DTO;
using Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfileController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfileController(IServiceManager serviceManager, UserManager<AppUser> userManager) : base(serviceManager)
        {
            _userManager = userManager;
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

        [HttpGet("{username}/topics")]
        public async Task<IActionResult> GetTopics(string username)
        {
            var topics = await ServiceManager.TopicService.GetAllByCreatorUsernameAsync(username);

            return Ok(topics);
        }
    }
}