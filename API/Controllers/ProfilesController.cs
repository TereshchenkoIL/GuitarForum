using System;
using System.Threading.Tasks;
using API.DTO;
using Contracts.Services;
using Domain.Entities;
using Domain.Exceptions.UserException;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        public ProfilesController(IServiceManager serviceManager, UserManager<AppUser> userManager) : base(serviceManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            var profile = await ServiceManager.ProfileService.GetDetails(username);

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UserUpdateDto userUpdateDto)
        {
            var profile = await ServiceManager.ProfileService.UpdateAsync(userUpdateDto.DisplayName, userUpdateDto.Bio);

            return Ok(profile);
        }

        [HttpGet("{username}/topics")]
        public async Task<IActionResult> GetTopics(string username)
        {
            var topics = await ServiceManager.TopicService.GetAllByCreatorUsernameAsync(username);
            return Ok(topics);
        }

        [HttpGet("{username}/activity")]
        public async Task<IActionResult> GetProfileActivity(string username)
        {
            var activity = await ServiceManager.ProfileService.GetProfileActivity(username);
            return Ok(activity);
        }
    }
}