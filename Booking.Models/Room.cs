namespace Booking.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public string? RoomType { get; set; }
        public bool IsAvailable { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
