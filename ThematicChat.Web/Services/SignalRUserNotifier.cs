using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Entities;
using ThematicChat.Core.Chatting.Interfaces;
using ThematicChat.Web.Hubs;

namespace ThematicChat.Web.Services
{
    public class SignalRUserNotifier : IUserNotifier
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SignalRUserNotifier(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyChatCreatedAsync(Chat chat)
        {
            await _hubContext.Clients.Clients(chat.Creator.ConnectionId, chat.JoinedUser.ConnectionId)
                .SendAsync("onChatCreated", chat);
        }

        public async Task NotifyChatPublishedAsync(ChatBoardItem chatBoardItem)
        {
            await _hubContext.Clients.All.SendAsync("onChatPublished", chatBoardItem);
        }

        public async Task NotifyChatUnpublishedAsync(string connectionId)
        {
            await _hubContext.Clients.All.SendAsync("onChatUnpublished", connectionId);
        }

        public async Task NotifyMessageSent(Chat chat, string message, string senderId)
        {
            await _hubContext.Clients.Clients(chat.Creator.ConnectionId, chat.JoinedUser.ConnectionId)
                .SendAsync("onMessageSent", message, senderId);
        }

        public async Task NotifyUserLeaveChat(Chat chat)
        {
            await _hubContext.Clients.Clients(chat.Creator.ConnectionId, chat.JoinedUser.ConnectionId)
                .SendAsync("onLeaveChat", chat);
        }
    }
}
