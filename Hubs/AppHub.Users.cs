using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public partial class AppHub : Hub<IAppHub>
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;

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
    }
}
