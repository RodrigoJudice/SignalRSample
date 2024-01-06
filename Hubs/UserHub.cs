using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class UserHub : Hub<IUserHub>
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;
        public static List<string> GroupsJoined { get; set; } = []; //lista de conexoes por group , controle

        public async Task<string> NewWindowLoaded(string Param1, string Param2)
        {
            TotalViews++;

            //send update to all clients that total view have been update
            await Clients.All
                .UpdateTotalViews(TotalViews);

            await Clients.All
                .UpdateDealthyHallowCount(SD.DealthyHallowRace);

            return $"Total views: {TotalViews} , Param1: {Param1} , Param2: {Param2}";
        }

        public async Task JoinHouse(string houseName)
        {

            var groupName = $"{Context.ConnectionId}:{houseName}"; //chave para associar

            if (GroupsJoined.Contains(groupName)) { return; }

            GroupsJoined.Add(groupName);

            string houseList = "";
            foreach (var str in GroupsJoined)
            {
                if (str.Contains(Context.ConnectionId))
                {
                    houseList += str.Split(':')[1] + " ";
                }
            }

            await Clients.Caller.SubscriptionStatus(houseList, houseName, true);
            await Clients.Others.InfomartionMembersToHouse(houseName, true);
            await Groups.AddToGroupAsync(Context.ConnectionId, houseName);

        }
        public async Task LeaveHouse(string houseName)
        {

            var groupName = $"{Context.ConnectionId}:{houseName}"; //chave para associar

            if (!GroupsJoined.Contains(groupName)) { return; }

            GroupsJoined.Remove(groupName);

            string houseList = "";
            foreach (var str in GroupsJoined)
            {
                if (str.Contains(Context.ConnectionId))
                {
                    houseList += str.Split(':')[1] + " ";
                }
            }

            await Clients.Caller.SubscriptionStatus(houseList, houseName, false);
            await Clients.Others.InfomartionMembersToHouse(houseName, false);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
        }



        public async Task TriggerHouseNotification(string houseName)
        {
            await Clients.Group(houseName).TriggerHouseNotification(houseName);
        }

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

    public interface IUserHub
    {
        Task UpdateTotalViews(int totalViews);
        Task UpdateTotalUsers(int TotalUsers);
        Task UpdateDealthyHallowCount(Dictionary<string, int> RaceStatus);
        Task SubscriptionStatus(string GroupsJoined, string houseName, bool HasSubscribed);
        Task InfomartionMembersToHouse(string houseName, bool HasSubscribed);
        Task TriggerHouseNotification(string houseName);




    }
}
