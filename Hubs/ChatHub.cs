using Microsoft.AspNetCore.SignalR;
using Services.ChatService;
using Models;
namespace Hubs
{
    public class ChatHub : Hub
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
            _service.AddConnectionId(name, Context.ConnectionId);
            await DisplayUsers();
        }

        private async Task DisplayUsers()
        {
            var onlineUsers = _service.GetUsers();
            await Clients.Groups("HiChat").SendAsync("Online Users", onlineUsers);
        }

        public async Task RecieveMessage(MessageInst message)
        {
            await Clients.Groups("HiChat").SendAsync("New Message", message);
        }

        public async Task CreatePrivateChat(MessageInst message)
        {
            string privateGroupName = GroupName(message.From, message.To);
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectId = _service.GetUserIdByConnectionName(message.To);
           
            await Groups.AddToGroupAsync(toConnectId, privateGroupName);
            await Clients.Client(toConnectId).SendAsync("OpenPChat", message);
        }

        public async Task RecievePrivateChat(MessageInst message)
        {
            string privateGroupName = GroupName(message.From, message.To);
            await Clients.Groups(privateGroupName).SendAsync("ReceivePChat", message);

        }

        public async Task RemovePrivateChat(string from, string to)
        {
            string privateGroupName = GroupName(from,to);
            await Clients.Groups(privateGroupName).SendAsync("ClosePChat");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
             string toId=_service.GetUserNameByConnectionId(to);
            await Groups.RemoveFromGroupAsync(toId, privateGroupName);
        }

        private string GroupName(string from, string? to)
        {
            bool cord = string.CompareOrdinal(from, to) < 0;
            return cord ? $"{from}-{to}" : $"{to}-{from}";
        }


    }
}