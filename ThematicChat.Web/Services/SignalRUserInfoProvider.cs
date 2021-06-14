using Microsoft.AspNetCore.Http;
using ThematicChat.Core.Chatting.Interfaces;

namespace ThematicChat.Web.Services
{
    public class SignalRUserInfoProvider : IUserInfoProvider
    {
        private readonly IHttpContextAccessor _context;

        public SignalRUserInfoProvider(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public string ConnectionId => _context.HttpContext.Connection.Id;
    }
}
