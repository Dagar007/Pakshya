using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.CommentCreateCommand command)
        {
            string email = GetEmail();
            command.Email = email;

            var comment = await _mediator.Send(command);

            await Clients.Group(command.PostId.ToString()).SendAsync("ReceiveComment", comment);
        }

        private string GetEmail()
        {
            return Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }

        public async Task AddToGroup(string groupName)
        {
            var username = GetEmail();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{username} has joined the group." );
        }
        public async Task RemoveFromGroup(string groupName)
        {
            var username = GetEmail();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{username} has the left group." );
        }


    }
}