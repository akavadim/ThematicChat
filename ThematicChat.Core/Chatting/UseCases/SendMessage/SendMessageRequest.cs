using MediatR;

namespace ThematicChat.Core.Chatting.UseCases.SendMessage
{
    public class SendMessageRequest : IRequest
    {
        public string ChatId { get; set; }
        public string Message { get; set; }
    }
}
