using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Infrastructure.Security
{
    public class IsAdminHandler: AuthorizationHandler<PermissionRequirement>
    {
        
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public IsAdminHandler( 
            IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
           var isAdmin =  context.User.IsInRole("Admin");
           
           if(isAdmin) context.Succeed(requirement);
           
           return Task.CompletedTask;
        }
    }
}