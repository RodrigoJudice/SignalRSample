using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public partial class AppHub : Hub<IAppHub>
    {
        public static int notificationCounter { get; set; } = 0;
        public static List<string> Notifications { get; set; } = [];

        public async Task SendNotification(string Message)
        {
            if (string.IsNullOrEmpty(Message)) return;

            notificationCounter++;
            Notifications.Add(Message);

            await Clients.All.ReceiveNotification(Message, notificationCounter);
        }

        public async Task LoadNotifications()
        {
            await Clients.Caller.LoadNotifications(Notifications, notificationCounter);
        }

    }
}
