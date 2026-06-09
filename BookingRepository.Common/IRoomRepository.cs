using Booking.Common;
using Booking.Models;


namespace Repository.Common
{
    public interface IRoomRepository
    {
        Task <Room> GetRoomByIdAsync(int id);
        Task <bool> CreateNewRoom(Room newRoom);
        Task <bool> UpdateRoom(int id, Room room);
        Task <bool> DeleteRoom(int id);

        Task<List<Room>> GetAllRoomsAsync(RoomFilter filter);
    }
}
