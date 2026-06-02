 using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private static List<Room> rooms = new List<Room>
        {
            new Room { RoomId = 1, Name = "Conference Room", Capacity = 10, RoomType = "Conference", IsAvailable = true },
            new Room { RoomId = 2, Name = "Meeting Room", Capacity = 5, RoomType = "Meeting", IsAvailable = false },
            new Room { RoomId = 3, Name = "Event Hall", Capacity = 50, RoomType = "Event", IsAvailable = true }
        };

        [HttpGet]
        public IActionResult GetAllRooms( [FromQuery] int? roomId = null, string roomType = null, bool? isAvailable = null)
        {
            List<Room> filteredRooms = rooms;
            if (roomId.HasValue)
            {
                filteredRooms.Where(room => room.RoomId == roomId.Value);
            }
            if (!string.IsNullOrEmpty(roomType))
            {
                filteredRooms.Where(room => room.RoomType.ToLower() == roomType.ToLower());
            }
            if (isAvailable.HasValue)
            {
                filteredRooms.Where(room => room.IsAvailable == isAvailable.Value);
            }
            if (filteredRooms.Count == 0)
            {
                return NotFound("No matching rooms found.");
            }

            return Ok(filteredRooms.ToList());
        }


        [HttpGet("{RoomId}")]
        public IActionResult GetRoomById(int roomId)
        {
            if(roomId <= 0)
            {
                return BadRequest("There is no such a room.");
            }
            Room room = rooms.FirstOrDefault(room => room.RoomId == roomId);
            if (room == null)
            {
                return NotFound("There is no rooms.");
            }
            return Ok(room);
        }


        [HttpPost]
        public Room PostCreateNewRoom([FromBody] Room newRoom)
        {
            newRoom.RoomId = rooms.Max(room => room.RoomId) + 1;
            rooms.Add(newRoom);
            return newRoom;
        }


        [HttpPut("{RoomId}")]
        public IActionResult PutUpdateRoom(int roomId, [FromBody] Room updatedRoom)
        {
            if(roomId <= 0)
            {
                return BadRequest("There is no rooms.");
            }
            if (updatedRoom == null)
            {
                return BadRequest("Updated room data is required.");
            }
            Room room = rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room == null){
                return null;
            }

            room.Capacity = updatedRoom.Capacity;
            room.IsAvailable = updatedRoom.IsAvailable;
            
            return Ok(room);
        }

        [HttpDelete("{RoomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            if(roomId <= 0)
            {
                return BadRequest("There is no rooms.");
            }

            Room room = rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room == null)
            {
                return NotFound("Room not found");
            }
            rooms.Remove(room);
            return Ok(room);
        }

     
    }
}
