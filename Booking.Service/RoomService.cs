using Booking.Models;
using Booking.Repository;
using Booking.WebApi.Models;
using BookingService.Common;
using Repository.Common;

namespace Booking.Service
{   
    public class RoomService: IRoomService
    {
        private IRoomRepository roomRepository;
        public RoomService()
        {
            roomRepository = new RoomRepository();
        }

        public List<Room> GetAllRooms(RoomFilter filter)
        {
            return roomRepository.GetAllRooms(filter);
        }

        public Room GetRoomById(int id)
        {
            return roomRepository.GetRoomById(id);
        }

        public bool CreateNewRoom(Room newRoom)
        {
            return roomRepository.CreateNewRoom(newRoom);
        }

        public bool UpdateRoom(int id, Room updatedRoom)
        {
            return roomRepository.UpdateRoom(id, updatedRoom);
        }

        public bool DeleteRoom(int id)
        {
            return roomRepository.DeleteRoom(id);
        }
    }
}
