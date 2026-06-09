namespace Booking.WebApi.RestModels
{
    public class ReservationRest
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string FullName { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsAvailable { get; set; }

    }
}

