using System;
using ThematicChat.Core.Enums;

namespace ThematicChat.Core.Chatting.Entities
{
    public class ChatBoardItem
    {
        public ChatBoardItem(
            User user,
            string title,
            ChatTypes type)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
            if (type == ChatTypes.Adult)
            {
                if (user.UserAge == AgeTypes.Under18)
                    throw new Exception("Несовершеннолетний пользователь");
                if (user.SearchableAges.Contains(AgeTypes.Under18))
                    throw new Exception("Нельзя искать несовершеннолетнего в взрослом чате");
            }

            User = user;
            Title = title;
            ChatType = type;

        }

        public User User { get; }
        public string Title { get; }
        public ChatTypes ChatType { get; }
    }
}
