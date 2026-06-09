using AutoMapper;
using Booking.Common;
using Booking.Models;
using Booking.Service;
using Booking.WebApi.RestModels;
using Microsoft.AspNetCore.Mvc;


namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        protected ReservationService reservationService { get; set; }
        private IMapper mapper { get; set; }
        public ReservationController(ReservationService resService, IMapper mapper)
        {
            reservationService = resService;
            mapper = mapper;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllReservationsAsync([FromQuery] ReservationFilter filter)
        {
            List<Reservation> reservations = await reservationService.GetAllReservationsAsync(filter);
            if(reservations.Count == 0)
            {
                return NotFound("No reservations.");
            }
            List<ReservationRest> reservationRests = mapper.Map<List<ReservationRest>>(reservations);
            return Ok(reservationRests);
        }


        [HttpGet("{Id}")]
        public async Task <IActionResult> GetReservationByIdAsync(int reservationId)
        {
           Reservation reservation = await reservationService.GetReservationByIdAsync(reservationId);
            if (reservation == null)
            {
                return NotFound("There is no reservations.");
            }
            ReservationRest reservationRest = mapper.Map<ReservationRest>(reservation);

            return Ok(reservation);
        }

        [HttpPost]
        public async Task <IActionResult> PostCreateNewReservation([FromBody] ReservationRest newReservationRest)
        {
            Reservation newReservation = mapper.Map<Reservation>(newReservationRest);
            bool isCreated = await reservationService.CreateNewReservation(newReservation);
            if ( !isCreated )
            {
                return BadRequest();
            }
            return Ok("Reservation successfully created.");
        }


        [HttpPut("{Id}")]
        public async Task <IActionResult> PutUpdateReservation(int reservationId, [FromBody] Reservation updatedReservation)
        {
            bool isUpdated = await reservationService.UpdateReservation(reservationId, updatedReservation);
            if (!isUpdated)
            {
                return NotFound("There is no reservations.");
            }
            return Ok("Reservation successfully updated.");
        }


        [HttpDelete("{Id}")]
        public async Task  <IActionResult> DeleteReservation(int id)
        {
            bool isDeleted = await reservationService.DeleteReservation(id);
            if ( !isDeleted )
            {
                return NotFound("There is no reservations.");
            }
            return Ok("Reservation successfully deleted.");
        }
    }
}

