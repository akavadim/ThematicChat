using System;
using ThematicChat.Core.Enums;

namespace ThematicChat.Core.Chatting.Entities
{
    public class Chat
    {
        public Chat(ChatBoardItem chatBoardItem, User user)
        {
            if (chatBoardItem == null)
                throw new ArgumentNullException(nameof(chatBoardItem));
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            ChatId = chatBoardItem.User.ConnectionId;
            Title = chatBoardItem.Title;
            ChatType = chatBoardItem.ChatType;
            Creator = chatBoardItem.User;
            JoinedUser = user;
        }

        public string ChatId { get; }
        public string Title { get; }
        public ChatTypes ChatType { get; }
        public User Creator { get; }
        public User JoinedUser { get; }

    }
}
