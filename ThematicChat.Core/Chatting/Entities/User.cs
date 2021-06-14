using System;
using System.Collections.Generic;
using ThematicChat.Core.Enums;

namespace ThematicChat.Core.Chatting.Entities
{
    public class User
    {
        public User(string userId,
            AgeTypes userAge,
            GenderTypes userGender,
            HashSet<AgeTypes> searchableAges,
            HashSet<GenderTypes> searchableGenders)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            if (searchableAges.Count == 0) throw new ArgumentOutOfRangeException(nameof(searchableAges));
            if (searchableGenders.Count == 0) throw new ArgumentOutOfRangeException(nameof(searchableGenders));

            ConnectionId = userId;
            UserAge = userAge;
            UserGender = userGender;
            SearchableAges = searchableAges;
            SearchableGenders = searchableGenders;
        }

        public string ConnectionId { get; }
        public AgeTypes UserAge { get; }
        public GenderTypes UserGender { get; }
        public HashSet<AgeTypes> SearchableAges { get; }
        public HashSet<GenderTypes> SearchableGenders { get; }
    }
}
