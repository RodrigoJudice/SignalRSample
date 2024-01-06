using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public partial class AppHub : Hub<IAppHub>
    {
        public static int messagesCounter { get; set; } = 0;
        public static List<string> Messages { get; set; } = [];

        public async Task SendMessage(string Message)
        {
            if (string.IsNullOrEmpty(Message)) return;

            messagesCounter++;
            Messages.Add(Message);

            await Clients.All.ReceiveMessage(Message, messagesCounter);
        }

        public async Task LoadMessages()
        {
            await Clients.Caller.LoadMessages(Messages, messagesCounter);
        }

    }
}
