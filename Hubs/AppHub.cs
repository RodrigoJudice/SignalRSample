using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public partial class AppHub : Hub<IAppHub>
    {

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.UpdateTotalUsers(TotalUsers);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.UpdateTotalUsers(TotalUsers);
            return base.OnDisconnectedAsync(exception);
        }

    }

    public interface IAppHub
    {
        Task UpdateTotalViews(int totalViews);
        Task UpdateTotalUsers(int TotalUsers);
        Task UpdateDealthyHallowCount(Dictionary<string, int> RaceStatus);
        Task SubscriptionStatus(string GroupsJoined, string houseName, bool HasSubscribed);
        Task InfomartionMembersToHouse(string houseName, bool HasSubscribed);
        Task TriggerHouseNotification(string houseName);
        Task ReceiveMessage(string Message, int messagesCounter);
        Task LoadMessages(List<string> Messages, int messagesCounter);
    }
}
