using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Current()
        {
            var user = await _userManager.Users.Include(x => x.Photo).FirstOrDefaultAsync(x => 
                x.Email == HttpContext.User.FindFirstValue(ClaimTypes.Email));

            if (user == null) return Unauthorized();
            var roles = await _userManager.GetRolesAsync(user);

            var isAdmin = roles.Contains("Admin");
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Image = user.Photo?.Url,
                Token = await _tokenService.CreateToken(user),
                isAdmin = isAdmin,
                Username = user.UserName
            });
           
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            var roles = await _userManager.GetRolesAsync(user);

            var isAdmin = roles.Contains("Admin");
            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = user.Photo?.Url,
                    Token = await _tokenService.CreateToken(user),
                    isAdmin = isAdmin,
                    Username = user.UserName
                };
            }

            return Unauthorized();
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Login(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user != null)
            {
                ModelState.AddModelError("email", "Email taken");
                return ValidationProblem(ModelState);
            }

            user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == registerDto.Username);

            if (user != null)
            {
                ModelState.AddModelError("username", "Username taken");
                return ValidationProblem(ModelState);
            }

            user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };


            var result = await _userManager.CreateAsync(user, registerDto.Password);

            
            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = user.Photo.Url,
                    Token = await _tokenService.CreateToken(user),
                    isAdmin = false,
                    Username = user.UserName
                };
            }

            return Unauthorized();
        }
        
    }
}