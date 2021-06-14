using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Core.Chatting.UseCases.UnpublishChat
{

    public class UnpublishChatHandler : IRequestHandler<UnpublishChatRequest>
    {
        private readonly IChatBoardRepository _chatBoardRepository;
        private readonly IUserNotifier _userNotifier;
        private readonly IUserInfoProvider _connectionIdGetter;

        public UnpublishChatHandler(
            IUserInfoProvider connectionIdGetter,
            IUserNotifier userNotifier,
            IChatBoardRepository chatBoardRepository)
        {
            _connectionIdGetter = connectionIdGetter;
            _userNotifier = userNotifier;
            _chatBoardRepository = chatBoardRepository;
        }

        public Task<Unit> Handle(UnpublishChatRequest request, CancellationToken cancellationToken)
        {
            if (_chatBoardRepository.Remove(_connectionIdGetter.ConnectionId))
                _userNotifier.NotifyChatUnpublishedAsync(_connectionIdGetter.ConnectionId);

            return Unit.Task;
        }
    }
}
