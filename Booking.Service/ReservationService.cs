using Booking.Common;
using Booking.Models;
using BookingRepository.Common;
using BookingService.Common;


namespace Booking.Service
{
    public class ReservationService : IReservationService
    {

        private IReservationRepository reservationRepository { get; set; }
        public ReservationService(IReservationRepository reservationRepo)
        {
            reservationRepository = reservationRepo;
        }

        public async Task <List<Reservation>> GetAllReservationsAsync(ReservationFilter filter)
        {
            return await reservationRepository.GetAllReservationsAsync(filter);
        }

        public async Task <Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await reservationRepository.GetReservationByIdAsync(reservationId);
        }

        public async Task <bool> CreateNewReservation(Reservation newReservation)
        {
            return await reservationRepository.CreateNewReservation(newReservation);
        }

        public async Task <bool> UpdateReservation(int reservationId, Reservation updatedReservation)
        {
            return await reservationRepository.UpdateReservation(reservationId, updatedReservation);
        }

        public async Task <bool> DeleteReservation(int reservationId)
        {
            return await reservationRepository.DeleteReservation(reservationId);
        }
    }
}
