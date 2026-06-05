using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private static List<Reservation> reservations = new List<Reservation>
        {
            new Reservation { ReservationId = 1, RoomId = 1, FullName = "Osoba1", ReservationDate = DateTime.Now, IsAvailable = true},
            new Reservation { ReservationId = 2, RoomId = 2, FullName = "Osoba2", ReservationDate = DateTime.Now, IsAvailable = false}
        };

        [HttpGet]
        public IActionResult GetAllReservations([FromQuery] int? reservationId = null, int? roomId = null, string? fullName = null ,bool? isActive = null)
        {
            
            IEnumerable<Reservation> filteredReservations = reservations;
            if (reservationId.HasValue)
            {
                filteredReservations.Where(reservation => reservation.ReservationId == reservationId.Value);
            }
            if (roomId.HasValue)
            {
                filteredReservations.Where(reservation => reservation.RoomId == roomId.Value);
            }
            if (!string.IsNullOrEmpty(fullName))
            {
                filteredReservations.Where(reservation => reservation.FullName.ToLower().Contains(fullName.ToLower()));
            }
            if (isActive.HasValue)
            {
                filteredReservations.Where(reservation => reservation.IsAvailable == isActive.Value);
            }

            List<Reservation> filteredReservationsList = filteredReservations.ToList();
            if (filteredReservationsList.Count == 0)
            {
                return NotFound("No matching reservations found.");
            }

            return Ok(filteredReservationsList);
        }


        [HttpGet("{ReservationId}")]
        public IActionResult GetReservationById(int reservationId)
        {
            if(reservationId <= 0)
            {
                return BadRequest("There is no reservations.");
            }
            Reservation reservation = reservations.FirstOrDefault(reservation => reservation.ReservationId == reservationId);
            if(reservation == null)
            {
                return NotFound("Reservation not found.");
            }
            return Ok(reservation);
        }

        [HttpPost]
        public Reservation PostCreateNewReservation([FromBody] Reservation newReservation)
        {
            newReservation.ReservationId = reservations.Max(reservation=>reservation.ReservationId) + 1;
            reservations.Add(newReservation);
            return newReservation;
        }

        [HttpPut("{ReservationId}")]
        public IActionResult PutUpdateReservation(int reservationId, [FromBody] Reservation updatedReservation)
        {
            if (reservationId <= 0)
            {
                return BadRequest("There is no reservations.");
            }
            if (updatedReservation == null)
            {
                return BadRequest("The reservation does not have any informations.");
            }

            Reservation reservation = reservations.FirstOrDefault(reservation => reservation.ReservationId == reservationId);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            reservation.RoomId = updatedReservation.RoomId;
            reservation.FullName = updatedReservation.FullName;
            reservation.ReservationDate = updatedReservation.ReservationDate;
            reservation.IsAvailable = updatedReservation.IsAvailable;

            return Ok(reservation);
        }

        [HttpDelete("{ReservationId}")]
        public IActionResult DeleteReservation(int id)
        {
            if (id <= 0)
            {
                return BadRequest("There is no reservations.");
            }

            Reservation reservation = reservations.FirstOrDefault(reservation => reservation.ReservationId == id);
            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            reservations.Remove(reservation);
            return Ok(reservation);
        }
    }
}
