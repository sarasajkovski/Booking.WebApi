using Booking.Common;
using Booking.Models;

namespace BookingService.Common
{
    public interface IReservationService
    {
        Task <Reservation> GetReservationByIdAsync(int id);
        Task <bool> CreateNewReservation(Reservation reservation);
        Task <bool> UpdateReservation(int id, Reservation reservation);
        Task <bool> DeleteReservation(int id);

        Task <List<Reservation>> GetAllReservationsAsync(ReservationFilter filter);
    }
}
