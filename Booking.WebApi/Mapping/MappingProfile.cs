using AutoMapper;
using Booking.Models;
using Booking.WebApi.RestModels;

namespace Booking.WebApi.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomRest>().ReverseMap();
            CreateMap<Reservation, ReservationRest>().ReverseMap();
        }
    }
}
