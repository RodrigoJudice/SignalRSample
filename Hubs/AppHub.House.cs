using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public partial class AppHub : Hub<IAppHub>
    {
        public static List<string> GroupsJoined { get; set; } = []; //lista de conexoes por group , controle

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
    }
}
