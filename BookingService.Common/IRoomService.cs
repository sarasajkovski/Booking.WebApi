using Booking.Models;
using Booking.WebApi.Models;

namespace BookingService.Common
{
    public interface IRoomService
    {
        Room GetRoomById(int id);
        bool CreateNewRoom(Room room);
        bool UpdateRoom(int id, Room room);
        bool DeleteRoom(int id);

        List<Room> GetAllRooms(RoomFilter filter);
    }
}
