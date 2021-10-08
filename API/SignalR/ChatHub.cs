using System;
using System.Threading.Tasks;
using API.DTO;
using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IServiceManager _serviceManager;

        public ChatHub(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task SendComment(CommentCreateDto commentDto)
        {
            var comment = await _serviceManager.CommentService.CreateAsync(commentDto);

            await Clients.Group(commentDto.Id.ToString()).SendAsync("ReceiveComment", comment);
        }

        public async Task UpdateComment(CommentUpdateDto commentDto)
        {
             await _serviceManager.CommentService.UpdateAsync(commentDto);

             var comment = await _serviceManager.CommentService.GetByIdAsync(commentDto.Id);

             await Clients.Group(commentDto.Id.ToString()).SendAsync("UpdateComment", comment);
        }
        
        public async Task DeleteComment(Guid id)
        {
            var comment = await _serviceManager.CommentService.GetByIdAsync(id);
            await _serviceManager.CommentService.DeleteAsync(id);

            await Clients.Group(comment.Id.ToString()).SendAsync("DeleteComment", id);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            var topicId = httpContext.Request.Query["topicId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, topicId);

            var comments = await _serviceManager.CommentService.GetAllByTopicAsync(Guid.Parse(topicId));

            await Clients.Caller.SendAsync("LoadComments", comments);


        }
    }
}