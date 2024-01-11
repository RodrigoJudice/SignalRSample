using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;
using SignalRSample.Models;
using System.Security.Claims;

namespace SignalRSample.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ChatRoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ChatRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChatRooms
        [HttpGet]
        [Route("GetChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoom()
        {
            if (_context.ChatRooms == null)
            {
                return NotFound();
            }
            return await _context.ChatRooms.ToListAsync();
        }

        [HttpGet]
        [Route("GetChatUser")]
        public async Task<ActionResult<Object>> GetChatUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return users.Where(u => u.Id != userId).Select(u => new { u.Id, u.UserName }).ToList();
        }




        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("PostChatRoom")]
        public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
            if (_context.ChatRooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChatRoom'  is null.");
            }
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

        // DELETE: api/ChatRooms/5
        [HttpDelete("{id}")]
        [Route("DeleteChatRoom/{id}")]
        public async Task<IActionResult> DeleteChatRoom(int id)
        {
            if (_context.ChatRooms == null)
            {
                return NotFound();
            }
            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            _context.ChatRooms.Remove(chatRoom);
            await _context.SaveChangesAsync();

            var room = await _context.ChatRooms.FirstOrDefaultAsync();

            return Ok(new { deleted = id, selected = (room == null ? 0 : room.Id) });
        }

    }

}
