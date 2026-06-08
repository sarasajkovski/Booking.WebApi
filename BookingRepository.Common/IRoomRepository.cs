using Booking.WebApi.Models;
using Booking.Models;


namespace Repository.Common
{
    public interface IRoomRepository
    {
        Room GetRoomById(int id);
        bool CreateNewRoom(Room newRoom);
        bool UpdateRoom(int id, Room room);
        bool DeleteRoom(int id);

        List<Room> GetAllRooms(RoomFilter filter);
    }
}
