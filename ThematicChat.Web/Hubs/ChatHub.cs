using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.UseCases.JoinChat;
using ThematicChat.Core.Chatting.UseCases.LeaveChat;
using ThematicChat.Core.Chatting.UseCases.PublishChat;
using ThematicChat.Core.Chatting.UseCases.SendMessage;
using ThematicChat.Core.Chatting.UseCases.UnpublishChat;

namespace ThematicChat.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishChat(PublishChatRequest request)
        {
            await _mediator.Send(request);
        }

        public async Task UnpublishChat()
        {
            await _mediator.Send(new UnpublishChatRequest());
        }

        public async Task JoinChat(JoinChatRequest joinChatRequest)
        {
            await _mediator.Send(joinChatRequest);
        }

        public async Task LeaveChat(LeaveChatRequest leaveChatRequest)
        {
            await _mediator.Send(leaveChatRequest);
        }

        public async Task SendMessage(SendMessageRequest sendMessageRequest)
        {
            await _mediator.Send(sendMessageRequest);
        }
    }
}