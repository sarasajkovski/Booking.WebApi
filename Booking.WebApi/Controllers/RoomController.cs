using AutoMapper;
using Booking.Common;
using Booking.Models;
using Booking.WebApi.RestModels;
using BookingService.Common;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        protected IRoomService roomService { get; set; }
        private IMapper mapper { get; set; }
        public RoomController(IRoomService roomServ, IMapper autoMapper)
        {
            roomService = roomServ;
            mapper = autoMapper;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllRoomsAsync([FromQuery] RoomFilter filter)
        {
            List<Room> rooms = await roomService.GetAllRoomsAsync(filter);
            List<RoomRest> roomRests = mapper.Map<List<RoomRest>>(rooms);

            if( roomRests.Count == 0)
            {
                return NotFound();
            }

            return Ok(roomRests);
        }


        [HttpGet("{Id}")]
        public async Task <IActionResult> GetRoomByIdAsync(int id)
        {
            Room room = await roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return BadRequest();
            }

            RoomRest roomRest = mapper.Map<RoomRest>(room);

            return Ok(roomRest);
        }


        [HttpPost]
        public async Task <IActionResult> PostCreateNewRoom([FromBody] RoomRest newRoomRest)
        {
            Room newRoom = mapper.Map<Room>(newRoomRest);
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
