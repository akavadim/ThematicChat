using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThematicChat.Core.Chatting.Entities;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Core.Chatting.UseCases.PublishChat
{

    public class PublishChatHandler : IRequestHandler<PublishChatRequest>
    {
        private readonly IChatBoardRepository _chatBoardRepository;
        private readonly IUserNotifier _userNotifier;
        private readonly IUserInfoProvider _userInfoProvider;

        public PublishChatHandler(IChatBoardRepository chatBoardRepository, IUserNotifier userNotifier, IUserInfoProvider userInfoProvider)
        {
            _chatBoardRepository = chatBoardRepository;
            _userNotifier = userNotifier;
            _userInfoProvider = userInfoProvider;
        }

        public async Task<Unit> Handle(PublishChatRequest request, CancellationToken cancellationToken)
        {
            ThrowExceptionIfUserHavePublishedChat(_userInfoProvider.ConnectionId);


            User user = new User(
                _userInfoProvider.ConnectionId,
                request.UserAge,
                request.UserGender,
                request.SearchableAges,
                request.SearchableGenders);
            ChatBoardItem chatBoardItem = new ChatBoardItem(user, request.Title, request.ChatType);

            _chatBoardRepository.Add(chatBoardItem);
            await _userNotifier.NotifyChatPublishedAsync(chatBoardItem);

            return await Unit.Task;
        }

        private void ThrowExceptionIfUserHavePublishedChat(string connectionId)
        {
            if (_chatBoardRepository.GetById(connectionId) != null)
                throw new Exception();
        }

        private void ThrowExceptionIfUserChatting(string connectionId)
        {
            if (_chatBoardRepository.GetById(connectionId) != null)
                throw new Exception();
        }

    }
}
