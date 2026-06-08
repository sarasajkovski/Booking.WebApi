using Booking.Models;
using Booking.WebApi.Models;

namespace BookingRepository.Common
{
    public interface IReservationRepository
    {
        Reservation GetReservationById(int id);
        bool CreateNewReservation(Reservation reservation);
        bool UpdateReservation(int id, Reservation reservation);
        bool DeleteReservation(int id);

        List<Reservation> GetAllReservations(ReservationFilter filter);
    }
}
