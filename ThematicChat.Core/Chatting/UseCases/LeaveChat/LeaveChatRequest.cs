using MediatR;

namespace ThematicChat.Core.Chatting.UseCases.LeaveChat
{
    public class LeaveChatRequest : IRequest
    {
        public string ChatId { get; set; }
    }
}
