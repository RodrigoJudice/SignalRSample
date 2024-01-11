using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;

namespace SignalRSample.Hubs
{
    public partial class AppHub : Hub<IAppHub>
    {
        private readonly ApplicationDbContext _db;

        public AppHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SendMessageToAll(string user, string message)
        {
            await Clients.All.MessageReceived(user, message);
        }

        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = await _db.Users.FirstOrDefaultAsync(u => u.Email!.ToLower() == receiver.ToLower());

            if (userId == null)
            {
                return;
            }
            await Clients.User(userId.Id).MessageReceived(sender, message);

        }
    }
}
