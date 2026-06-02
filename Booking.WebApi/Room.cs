namespace Booking.WebApi
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }   
    }
}
