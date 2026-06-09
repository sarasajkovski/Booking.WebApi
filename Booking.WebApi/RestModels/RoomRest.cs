namespace Booking.WebApi.RestModels
{
    public class RoomRest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; }
        public bool IsAvailable { get; set; }
    }
}

