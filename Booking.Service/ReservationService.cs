using Booking.Models;
using Booking.Repository;
using Booking.WebApi.Models;
using BookingRepository.Common;
using BookingService.Common;


namespace Booking.Service
{
    public class ReservationService : IReservationService
    {

        private IReservationRepository reservationRepository;
        public ReservationService()
        {
            reservationRepository = new ReservationRepository();
        }

        public List<Reservation> GetAllReservations(ReservationFilter filter)
        {
            return reservationRepository.GetAllReservations(filter);
        }

        public Reservation GetReservationById(int reservationId)
        {
            return reservationRepository.GetReservationById(reservationId);
        }

        public bool CreateNewReservation(Reservation newReservation)
        {
            return reservationRepository.CreateNewReservation(newReservation);
        }

        public bool UpdateReservation(int reservationId, Reservation updatedReservation)
        {
            return reservationRepository.UpdateReservation(reservationId, updatedReservation);
        }

        public bool DeleteReservation(int reservationId)
        {
            return reservationRepository.DeleteReservation(reservationId);
        }
    }
}
