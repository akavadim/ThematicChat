using System.Collections.Generic;
using System.Linq;
using ThematicChat.Core.Chatting.Entities;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Core.ChatBoard
{
    public class ChatBoardRepository : IChatBoardRepository
    {
        private readonly Dictionary<string, ChatBoardItem> _db;

        public ChatBoardRepository()
        {
            _db = new Dictionary<string, ChatBoardItem>();
        }

        public void Add(ChatBoardItem chatBoardItem)
        {
            _db.Add(chatBoardItem.User.ConnectionId, chatBoardItem);
        }

        public ChatBoardItem[] GetAll()
        {
            return _db.Values.ToArray();
        }

        public ChatBoardItem GetById(string connectionId)
        {
            ChatBoardItem chatBoardItem = null;
            _db.TryGetValue(connectionId, out chatBoardItem);
            return chatBoardItem;
        }

        public bool Remove(string connectionId)
        {
            return _db.Remove(connectionId);
        }
    }
}
