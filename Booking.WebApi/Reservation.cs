namespace Booking.WebApi
{
    public class Reservation
    {
        public int ReservationId { get; set; } // primary key
        public int RoomId { get; set; } // bit će foreign key prema Room 
        public string FullName { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsAvailable { get; set; }

    }
}
