using ThematicChat.Core.Chatting.Entities;

namespace ThematicChat.Core.Chatting.Interfaces
{
    public interface IChatBoardRepository
    {
        void Add(ChatBoardItem chatBoardItem);
        bool Remove(string userId);
        ChatBoardItem[] GetAll();
        ChatBoardItem GetById(string userId);
    }
}
