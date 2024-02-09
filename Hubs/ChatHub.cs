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
            await base.OnDisconnectedAsync(ex);
        }

    }
}