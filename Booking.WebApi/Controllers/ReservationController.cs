using Booking.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private string connectionString = "Host=localhost;Port=5432;Database=booking;Username=postgres;Password=jk7pVHZ5";

        [HttpGet]
        public IActionResult GetAllReservations([FromQuery] int? Id = null, int? roomId = null, string? fullName = null, bool? isAvailable = null)
        {
            try
            {
                List<Reservation> reservations = new List<Reservation>();

                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                StringBuilder sb = new StringBuilder();

                sb.Append(@"SELECT * FROM ""Reservation"" WHERE 1 = 1");
                if (Id.HasValue)
                {
                    sb.Append(@" AND Id = @reservationId");
                }
                if (roomId.HasValue)
                {
                    sb.Append(@"AND roomId = @roomId");
                }
                if(!string.IsNullOrEmpty(fullName))
                {
                    sb.Append(@"AND fullName = @fullName");
                }
                if (isAvailable.HasValue) { 
                    sb.Append(@"AND isAvailable = @isAvailable");
                }
                string commandText = sb.ToString();
                
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                if (Id.HasValue)
                {
                    command.Parameters.AddWithValue("@reservationId", Id.Value);
                }
                if (roomId.HasValue)
                {
                    command.Parameters.AddWithValue("@roomId", roomId.Value);
                }
                if (!string.IsNullOrEmpty(fullName))
                {
                    command.Parameters.AddWithValue("@fullName", fullName);
                }
                if (isAvailable.HasValue)
                {
                    command.Parameters.AddWithValue("@isAvailable", isAvailable.Value);
                }

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Reservation reservation = new Reservation
                    {
                        Id = reader.GetInt32(0),
                        RoomId = reader.GetInt32(1),
                        FullName = reader.GetString(2),
                        ReservationDate = reader.GetDateTime(3),
                        IsAvailable = reader.GetBoolean(4)
                    };
                    reservations.Add(reservation);
                }

                connection.Close();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{Id}")]
        public IActionResult GetReservationById(int reservationId)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = "SELECT * FROM \"Reservation\" WHERE id = @reservationId";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@reservationId", reservationId);

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                bool hasData = reader.Read();
                if (!hasData)
                {
                    return NotFound();
                }

                Reservation reservation = new Reservation
                {
                    Id = int.Parse(reader["id"].ToString()),
                    RoomId = int.Parse(reader["room_id"].ToString()),
                    FullName = reader["\"fullName\""].ToString(),
                    ReservationDate = DateTime.Parse(reader["\"reservationDate\""].ToString()),
                    IsAvailable = bool.Parse(reader["\"isAvailable\""].ToString())
                };

                connection.Close();
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostCreateNewReservation([FromBody] Reservation newReservation)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText =
                    "INSERT INTO \"Reservation\" (room_id, ˇ\"fullName\", \"reservationDate\", \"isAvailable\") VALUES (@roomId, @fullName, @reservationDate, @isAvailable)";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);


                command.Parameters.AddWithValue("@roomId", newReservation.RoomId);
                command.Parameters.AddWithValue("@fullName", newReservation.FullName);
                command.Parameters.AddWithValue("@reservationDate", newReservation.ReservationDate);
                command.Parameters.AddWithValue("@isAvailable", newReservation.IsAvailable);

                connection.Open();
                int numberOfRowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsAffected == 0)
                {
                    return Ok("Unfortunately, reservation failed.");
                }
                return Ok("Reservation successfully created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{Id}")]
        public IActionResult PutUpdateReservation(int reservationId, [FromBody] Reservation updatedReservation)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText =
                    $"UPDATE \"Reservation\" SET room_id = @roomId, fullName = @fullName, reservationDate = @reservationDate, isAvailable = @isAvailable WHERE reservation_id = @reservationId";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@roomId", updatedReservation.RoomId);
                command.Parameters.AddWithValue("@fullName", updatedReservation.FullName);
                command.Parameters.AddWithValue("@reservationDate", updatedReservation.ReservationDate);
                command.Parameters.AddWithValue("@isAvailable", updatedReservation.IsAvailable);
                command.Parameters.AddWithValue("@reservationId", reservationId);

                connection.Open();
                int numberOfRowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsAffected == 0)
                {
                    return NotFound("There is no reservations.");
                }
                return Ok("Reservation successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = $"DELETE FROM \"Reservation\" WHERE id = @reservationId";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@reservationId", id);
                connection.Open();
                int numberOfRowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsAffected == 0)
                {
                    return NotFound("There is no reservations.");
                }
                return Ok("Reservation successfully deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

