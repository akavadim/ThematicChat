using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Core.Chatting.UseCases.LeaveChat
{
    public class LeaveChatHandler : IRequestHandler<LeaveChatRequest>
    {
        private readonly IUserInfoProvider _userInfoProvider;
        private readonly IChatRepository _chatRepository;
        private readonly IUserNotifier _userNotifier;

        public LeaveChatHandler(IUserInfoProvider userInfoProvider, IChatRepository chatRepository)
        {
            _userInfoProvider = userInfoProvider;
            _chatRepository = chatRepository;
        }

        public Task<Unit> Handle(LeaveChatRequest request, CancellationToken cancellationToken)
        {
            var chat = _chatRepository.GetById(request.ChatId);
            if (chat == null)
                throw new Exception();

            if (!(chat.Creator.ConnectionId == _userInfoProvider.ConnectionId ||
                chat.JoinedUser.ConnectionId == _userInfoProvider.ConnectionId))
                throw new Exception("User не состоит в чате");

            if (_chatRepository.Remove(chat.ChatId))
                _userNotifier.NotifyUserLeaveChat(chat);

            return Unit.Task;
        }
    }
}
