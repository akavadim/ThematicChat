using ThematicChat.Core.Chatting.Entities;

namespace ThematicChat.Core.Chatting.Interfaces
{
    public interface IChatRepository
    {
        Chat[] GetAll();
        Chat GetById(string chatId);
        void Add(Chat chat);
        bool Remove(string chatId);
    }
}
