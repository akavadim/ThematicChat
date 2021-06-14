using MediatR;
using System.Collections.Generic;
using ThematicChat.Core.Enums;

namespace ThematicChat.Core.Chatting.UseCases.JoinChat
{
    public class JoinChatRequest : IRequest
    {
        public string UserId { get; set; }

        public AgeTypes UserAge { get; set; }
        public GenderTypes UserGender { get; set; }
        public HashSet<AgeTypes> SearchableAges { get; set; }
        public HashSet<GenderTypes> SearchableGenders { get; set; }
    }
}
