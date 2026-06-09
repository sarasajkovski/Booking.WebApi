namespace Booking.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string? FullName { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool? IsAvailable { get; set; }

        public Room? Room { get; set; }
    }
}
