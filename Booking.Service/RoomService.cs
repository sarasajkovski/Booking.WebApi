using Booking.Common;

using Booking.Models;
using BookingService.Common;
using Repository.Common;

namespace Booking.Service
{   
    public class RoomService: IRoomService
    {
        protected IRoomRepository RoomRepository { get; set; }
        public RoomService(IRoomRepository roomRepo)
        {
            RoomRepository = roomRepo;
        }

        public async Task<List<Room>> GetAllRoomsAsync(RoomFilter filter)
        {
            return await RoomRepository.GetAllRoomsAsync(filter);
        } 

        public async Task <Room> GetRoomByIdAsync(int id)
        {
            return await RoomRepository.GetRoomByIdAsync(id);
        }

        public async Task <bool> CreateNewRoom(Room newRoom)
        {
            return await RoomRepository.CreateNewRoom(newRoom);
        }

        public async Task <bool> UpdateRoom(int id, Room updatedRoom)
        {
            return await RoomRepository.UpdateRoom(id, updatedRoom);
        }

        public async Task <bool> DeleteRoom(int id)
        {
            return await RoomRepository.DeleteRoom(id);
        }
    }
}
