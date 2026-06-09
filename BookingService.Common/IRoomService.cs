using Booking.Common;
using Booking.Models;

namespace BookingService.Common
{
    public interface IRoomService
    {
        Task <Room> GetRoomByIdAsync(int id);
        Task <bool> CreateNewRoom(Room room);
        Task <bool> UpdateRoom(int id, Room room);
        Task <bool> DeleteRoom(int id);

        Task <List<Room>> GetAllRoomsAsync(RoomFilter filter);
    }
}
