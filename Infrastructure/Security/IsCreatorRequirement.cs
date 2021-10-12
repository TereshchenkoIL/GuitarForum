using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security
{
    public class IsCreatorRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DataContext _context;

        public IsCreatorRequirementHandler(DataContext context, 
            IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(userId == null)
                return Task.CompletedTask;

            var topicId = Guid.Parse(_contextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "topicId").Value?.ToString() ?? string.Empty);


            var topic = _context.Topics.Include(c => c.Creator)
                .FirstOrDefault(t => t.Id == topicId);

            if (topic == null) return Task.CompletedTask;
            
            if(topic.Creator.Id == userId) context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}