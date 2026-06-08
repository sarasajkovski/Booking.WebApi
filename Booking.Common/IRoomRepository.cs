using Booking.WebApi.Models;


namespace Repository.Common
{
    public interface IRoomRepository
    {
        Room GetRoomById(int id);
        bool CreateRoom(Room room);
        bool UpdateRoom(int id, Room room)
        bool DeleteRoom(int id);

        List<Room> GetAllRooms(RoomFilter filter);
    }
}
