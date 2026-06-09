using Booking.Common;
using Booking.Models;

namespace BookingRepository.Common
{
    public interface IReservationRepository
    {
        Task <Reservation> GetReservationByIdAsync(int id);
        Task <bool> CreateNewReservation(Reservation reservation);
        Task <bool> UpdateReservation(int id, Reservation reservation);
        Task <bool> DeleteReservation(int id);

        Task <List<Reservation>> GetAllReservationsAsync(ReservationFilter filter);
    }
}
