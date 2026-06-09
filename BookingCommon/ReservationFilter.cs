
namespace Booking.Common
{
    public class ReservationFilter
    {
        public int? Id { get; set; }
        public int? RoomId { get; set; }
        public string? FullName { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
