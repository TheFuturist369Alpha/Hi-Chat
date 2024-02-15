using Microsoft.AspNetCore.SignalR;
using Services.ChatService;

namespace Hubs
{
    public class ChatHub:Hub
    {
        private readonly ChatServe _service;

        public ChatHub(ChatServe service)
        {
            _service = service;
        }

        public override async Task OnConnectedAsync()
        
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "HiChat");
            await Clients.Caller.SendAsync("User Connected.");
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "HiChat");
            var user = _service.GetUserNameByConnectionId(Context.ConnectionId);
            _service.RemoveUserByName(user);
           await DisplayUsers();
            await base.OnDisconnectedAsync(ex);
        }
        public async Task AddUserConnectionId(string name)
        {
            _service.AddConnectionId(name,Context.ConnectionId);
          await DisplayUsers();
        }
        private async Task DisplayUsers()
        {
            var onlineUsers = _service.GetUsers();
            await Clients.Groups("HiChat").SendAsync("Online Users", onlineUsers);
        }
    }
}