using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Entities;

namespace ThematicChat.Core.Chatting.Interfaces
{
    public interface IUserNotifier
    {
        Task NotifyChatPublishedAsync(ChatBoardItem chatBoardItem);
        Task NotifyChatUnpublishedAsync(string userId);
        Task NotifyChatCreatedAsync(Chat chat);
        Task NotifyUserLeaveChat(Chat chat);
        Task NotifyMessageSent(Chat chat, string message, string senderId);
    }
}
