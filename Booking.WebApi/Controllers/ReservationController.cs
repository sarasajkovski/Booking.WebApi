using Booking.Models;
using Booking.Service;
using Booking.WebApi.Models;
using Microsoft.AspNetCore.Mvc;


namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private ReservationService reservationService = new ReservationService();

        [HttpGet]
        public IActionResult GetAllReservations([FromQuery] ReservationFilter filter)
        {
            List<Reservation> reservations = reservationService.GetAllReservations(filter);
            if(reservations.Count == 0)
            {
                return NotFound("No reservations.");
            }
            return Ok(reservations);
        }


        [HttpGet("{Id}")]
        public IActionResult GetReservationById(int reservationId)
        {
           Reservation reservation = reservationService.GetReservationById(reservationId);
            if (reservation == null)
            {
                return NotFound("There is no reservations.");
            }
            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult PostCreateNewReservation([FromBody] Reservation newReservation)
        {
           bool isCreated = reservationService.CreateNewReservation(newReservation);
            if ( !isCreated )
            {
                return BadRequest();
            }
            return Ok("Reservation successfully created.");
        }


        [HttpPut("{Id}")]
        public IActionResult PutUpdateReservation(int reservationId, [FromBody] Reservation updatedReservation)
        {
            bool isUpdated = reservationService.UpdateReservation(reservationId, updatedReservation);
            if (!isUpdated)
            {
                return NotFound("There is no reservations.");
            }
            return Ok("Reservation successfully updated.");
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteReservation(int id)
        {
            bool isDeleted = reservationService.DeleteReservation(id);
            if ( !isDeleted )
            {
                return NotFound("There is no reservations.");
            }
            return Ok("Reservation successfully deleted.");
        }
    }
}

