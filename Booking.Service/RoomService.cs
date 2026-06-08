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

        public async Task<List<Room>> GetAllRoomsAsync(RoomFilter filter)
        {
            return await roomRepository.GetAllRoomsAsync(filter);
        }

        public async Task <Room> GetRoomByIdAsync(int id)
        {
            return await roomRepository.GetRoomByIdAsync(id);
        }

        public async Task <bool> CreateNewRoom(Room newRoom)
        {
            return await roomRepository.CreateNewRoom(newRoom);
        }

        public async Task <bool> UpdateRoom(int id, Room updatedRoom)
        {
            return await roomRepository.UpdateRoom(id, updatedRoom);
        }

        public async Task <bool> DeleteRoom(int id)
        {
            return await roomRepository.DeleteRoom(id);
        }
    }
}
