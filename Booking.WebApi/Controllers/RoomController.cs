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
        public IActionResult GetAllRooms([FromQuery] RoomFilter filter)
        {
            List<Room> rooms = roomService.GetAllRooms(filter);
            if(rooms.Count == 0)
            {
                return NotFound("No rooms.");
            }
            return Ok(rooms);
        }


        [HttpGet("{Id}")]
        public IActionResult GetRoomById(int id)
        {
            Room room = roomService.GetRoomById(id);
            if (room == null)
            {
                return BadRequest();
            }
            return Ok(room);
        }


        [HttpPost]
        public IActionResult PostCreateNewRoom([FromBody] Room newRoom)
        {
            bool isCreated = roomService.CreateNewRoom(newRoom);
            if ( !isCreated )
            {
                return BadRequest();
            }
            return Ok("Room successfully created.");
        }


        [HttpPut("{Id}")]
        public IActionResult PutUpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            bool isUpdated = roomService.UpdateRoom(id, updatedRoom);
            if (!isUpdated)
            {
                return BadRequest();
            }
            return Ok("Room successfully created.");
        }



        [HttpDelete("{Id}")]
        public IActionResult DeleteRoom(int id)
        {
            bool isDeleted = roomService.DeleteRoom(id);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok("Room successfully deleted.");
        }
    }
}
