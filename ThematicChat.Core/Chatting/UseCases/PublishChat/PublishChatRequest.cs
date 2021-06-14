using MediatR;
using System.Collections.Generic;
using ThematicChat.Core.Enums;

namespace ThematicChat.Core.Chatting.UseCases.PublishChat
{
    public class PublishChatRequest : IRequest
    {
        public string Title { get; set; }
        public ChatTypes ChatType { get; set; }
        public AgeTypes UserAge { get; set; }
        public GenderTypes UserGender { get; set; }
        public HashSet<AgeTypes> SearchableAges { get; set; }
        public HashSet<GenderTypes> SearchableGenders { get; set; }
    }
}
