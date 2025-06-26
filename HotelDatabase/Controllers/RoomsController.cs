using HotelDatabase.Interfaces;
using HotelDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<IActionResult> GetAvailableRooms()
        {
            var rooms = await _roomService.GetAvailableRoomsAsync();
            return Ok(rooms);
        }

        // GET: api/Rooms/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] Room room)
        {
            var createdRoom = await _roomService.CreateRoomAsync(room);
            return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
        }

        // PUT: api/Rooms/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var success = await _roomService.UpdateRoomAsync(id, updatedRoom);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/Rooms/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var success = await _roomService.DeleteRoomAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpGet("group-by-type")]
        public async Task<IActionResult> GetRoomCountByType()
        {
            try
            {
                var result = await _roomService.GetRoomGroupByTypeAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // POST: api/Rooms/check-availability
        [HttpPost("check-availability")]
        public async Task<IActionResult> CheckAvailability([FromBody] UserRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request?.RoomType))
                    return BadRequest("RoomType is required.");

                var availableRooms = await _roomService.CheckRoomAvailabilityAsync(request.RoomType);

                if (!availableRooms.Any())
                    return NotFound("No available rooms of the specified type.");

                return Ok(availableRooms.Select(r => new
                {
                    r.Id,
                    r.RoomNumber,
                    r.Type,
                    r.Price,
                    r.Features
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}