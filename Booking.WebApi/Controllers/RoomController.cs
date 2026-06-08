using Booking.Models;
using Booking.Service;
using Booking.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {

        private RoomService roomService = new RoomService();

        [HttpGet]
        public async Task <IActionResult> GetAllRoomsAsync([FromQuery] RoomFilter filter)
        {
            List<Room> rooms = await roomService.GetAllRoomsAsync(filter);
            if(rooms.Count == 0)
            {
                return NotFound("No rooms.");
            }
            return Ok(rooms);
        }


        [HttpGet("{Id}")]
        public async Task <IActionResult> GetRoomByIdAsync(int id)
        {
            Room room = await roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return BadRequest();
            }
            return Ok(room);
        }


        [HttpPost]
        public async Task <IActionResult> PostCreateNewRoom([FromBody] Room newRoom)
        {
            bool isCreated = await roomService.CreateNewRoom(newRoom);
            if ( !isCreated )
            {
                return BadRequest();
            }
            return Ok("Room successfully created.");
        }


        [HttpPut("{Id}")]
        public async Task <IActionResult> PutUpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            bool isUpdated = await roomService.UpdateRoom(id, updatedRoom);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok("Room successfully created.");
        }



        [HttpDelete("{Id}")]
        public async Task <IActionResult> DeleteRoom(int id)
        {
            bool isDeleted = await roomService.DeleteRoom(id);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok("Room successfully deleted.");
        }
    }
}
