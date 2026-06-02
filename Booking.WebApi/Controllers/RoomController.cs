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
        public List<Room> GetAllRooms()
        {
            if (rooms.Count == 0)
            {
                return null;
            }
            return rooms;
        }


        [HttpGet("{RoomId}")]
        public Room GetRoomById(int roomId)
        {
            return rooms.FirstOrDefault(room => room.RoomId == roomId);
        }


        [HttpPost]
        public Room PostCreateNewRoom([FromBody] Room newRoom)
        {
            newRoom.RoomId = rooms.Max(room => room.RoomId) + 1;
            rooms.Add(newRoom);
            return newRoom;
        }


        [HttpPut("{RoomId}")]
        public Room PutUpdateRoom(int roomId, [FromBody] Room updatedRoom)
        {
            Room room = rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room == null){
                return null;
            }
            room.Capacity = updatedRoom.Capacity;
            room.IsAvailable = updatedRoom.IsAvailable;
            
            return room;
        }

        [HttpDelete("{RoomId}")]
        public Room DeleteRoom(int roomId)
        {
            Room room = rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room == null)
            {
                return null;
            }
            rooms.Remove(room);
            return room;
        }

        [HttpGet("available")]
        public List<Room> GetAvailableRooms([FromQuery] bool isAvailable)
        {
            if(rooms.Count == 0)
            {
                return null;
            }
            return rooms.Where(room => room.IsAvailable == isAvailable).ToList();
        }
    }
}
