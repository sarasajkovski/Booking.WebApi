using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private static List<Reservation> reservations = new List<Reservation>
        {
            new Reservation { ReservationId = 1, RoomId = 1, FullName = "Sara Sajkovski", ReservationtDate = DateTime.Now, IsAvailable = true}
        };

        [HttpGet]
        public List<Reservation> GetAllReservations()
        {
            if (reservations.Count == 0)
            {
                return null;
            }
            return reservations;
        }

        [HttpGet("{ReservationId}")]
        public Reservation GetReservationById(int ReservationId)
        {
            return reservations.FirstOrDefault(reservation => reservation.ReservationId == ReservationId);
        }

        [HttpPost]
        public Reservation PostCreateNewReservation([FromBody] Reservation newReservation)
        {
            newReservation.ReservationId = reservations.Max(reservation=>reservation.ReservationId)+1;
            reservations.Add(newReservation);
            return newReservation;
        }

        [HttpPut("{ReservationId}")]
        public Reservation PutUpdateReservation(int reservationId, [FromBody] Reservation updatedReservation)
        {
            Reservation reservation = reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation == null){
                return null;
            }
            reservation.ReservationtDate = updatedReservation.ReservationtDate;
            reservation.IsAvailable = updatedReservation.IsAvailable;
            return reservation;
        }

        [HttpDelete("{ReservationId}")]
        public Reservation DeleteReservation(int reservationId)
        {
            Reservation reservation = reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation == null){
                return null;
            }
            reservations.Remove(reservation);
            return reservation;
        }
    }
}
