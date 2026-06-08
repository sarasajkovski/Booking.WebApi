using Booking.Models;
using Booking.WebApi.Models;

namespace BookingService.Common
{
    public interface IReservationService
    {
        Reservation GetReservationById(int id);
        bool CreateNewReservation(Reservation reservation);
        bool UpdateReservation(int id, Reservation reservation);
        bool DeleteReservation(int id);

        List<Reservation> GetAllReservations(ReservationFilter filter);
    }
}
