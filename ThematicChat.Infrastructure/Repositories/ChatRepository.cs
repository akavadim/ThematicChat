using System.Collections.Generic;
using System.Linq;
using ThematicChat.Core.Chatting.Entities;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly Dictionary<string, Chat> _db;

        public ChatRepository()
        {
            _db = new Dictionary<string, Chat>();
        }

        public void Add(Chat chat)
        {
            _db.Add(chat.ChatId, chat);
        }

        public Chat[] GetAll()
        {
            return _db.Values.ToArray();
        }

        public Chat GetById(string chatId)
        {
            Chat chat;
            _db.TryGetValue(chatId, out chat);
            return chat;
        }

        public bool Remove(string chatId)
        {
            return _db.Remove(chatId);
        }
    }
}
