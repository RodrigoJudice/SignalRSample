using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Hubs;

namespace SignalRSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeathlyHallowsController : ControllerBase
    {
        private readonly IHubContext<UserHub, IUserHub> _userHub;

        public DeathlyHallowsController(IHubContext<UserHub, IUserHub> userHub)
        {
            _userHub = userHub;
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


            await _userHub.Clients.All
                .UpdateDealthyHallowCount(SD.DealthyHallowRace);

            return Accepted();
        }
    }
}
