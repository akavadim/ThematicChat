using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Entities;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Core.Chatting.UseCases.JoinChat
{
    public class JoinChatHandler : IRequestHandler<JoinChatRequest>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatBoardRepository _chatBoardRepository;
        private readonly IUserNotifier _userNotifier;
        private readonly IUserInfoProvider _userInfoProvider;

        public JoinChatHandler(IUserInfoProvider userInfoProvider,
            IUserNotifier userNotifier,
            IChatBoardRepository chatBoardRepository,
            IChatRepository chatRepository)
        {
            _userInfoProvider = userInfoProvider;
            _userNotifier = userNotifier;
            _chatBoardRepository = chatBoardRepository;
            _chatRepository = chatRepository;
        }

        public async Task<Unit> Handle(JoinChatRequest request, CancellationToken cancellationToken)
        {
            ThrowExceptionIfUserHavePublishedChat(_userInfoProvider.ConnectionId);
            ThrowExceptionIfUserChatting(_userInfoProvider.ConnectionId);

            ChatBoardItem publishedChat = _chatBoardRepository.GetById(request.UserId) ?? throw new Exception("Чат с заданным ID не найден");

            User user = new User(_userInfoProvider.ConnectionId,
                request.UserAge,
                request.UserGender,
                request.SearchableAges,
                request.SearchableGenders);

            Chat chat = new Chat(publishedChat, user);
            _chatRepository.Add(chat);

            await _userNotifier.NotifyChatCreatedAsync(chat);

            return await Unit.Task;
        }

        private void ThrowExceptionIfUserHavePublishedChat(string userId)
        {
            if (_chatBoardRepository.GetById(userId) != null)
                throw new Exception();
        }

        private void ThrowExceptionIfUserChatting(string userId)
        {
            if (_chatBoardRepository.GetById(userId) != null)
                throw new Exception();
        }
    }
}
