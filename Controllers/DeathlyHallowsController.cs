using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;

namespace SignalRSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeathlyHallowsController : ControllerBase
    {
        private readonly IHubContext<Hubs.AppHub, IAppHub> _appHub;

        public DeathlyHallowsController(IHubContext<Hubs.AppHub, IAppHub> appHub)
        {
            _appHub = appHub;
        }

        [HttpGet]
        public async Task<IActionResult> Vote(string type)
        {
            if (type is null)
            {
                return BadRequest();
            }

            if (SD.DealthyHallowRace.TryGetValue(type, out int value))
            {
                SD.DealthyHallowRace[type] = ++value;
            }


            await _appHub.Clients.All
                .UpdateDealthyHallowCount(SD.DealthyHallowRace);

            return Accepted();
        }
    }
}
