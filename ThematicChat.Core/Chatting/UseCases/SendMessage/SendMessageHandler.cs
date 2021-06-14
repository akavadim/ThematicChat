using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Core.Chatting.UseCases.SendMessage
{
    public class SendMessageHandler : IRequestHandler<SendMessageRequest>
    {
        private readonly IUserInfoProvider _userInfoProvider;
        private readonly IUserNotifier _userNotifier;
        private readonly IChatRepository _chatRepository;

        public SendMessageHandler(IUserInfoProvider userInfoProvider, IUserNotifier userNotifier, IChatRepository chatRepository)
        {
            _userInfoProvider = userInfoProvider;
            _userNotifier = userNotifier;
            _chatRepository = chatRepository;
        }

        public Task<Unit> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            var chat = _chatRepository.GetById(request.ChatId);
            if (chat == null)
                throw new Exception("чат с таким Id не найден");
            if (!(chat.Creator.ConnectionId == _userInfoProvider.ConnectionId ||
                chat.JoinedUser.ConnectionId == _userInfoProvider.ConnectionId))
                throw new Exception("Пользователь не состоит в чате");

            _userNotifier.NotifyMessageSent(chat, request.Message, _userInfoProvider.ConnectionId);

            return Unit.Task;
        }
    }
}
