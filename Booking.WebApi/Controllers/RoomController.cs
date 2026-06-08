using Booking.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private string connectionString = "Host=localhost;Port=5432;Database=booking;Username=postgres;Password=jk7pVHZ5";

        [HttpGet]
        public IActionResult GetAllRooms([FromQuery] int? Id = null, string? roomType = null, bool? isAvailable = null)
        {
            try
            {
                List<Room> rooms = new List<Room>();

                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                StringBuilder sb = new StringBuilder();

                sb.Append(@"SELECT * FROM ""Room"" WHERE 1=1");
                if (Id.HasValue)
                {
                    sb.Append(@" AND Id = @roomId");
                }
                if (!string.IsNullOrEmpty(roomType))
                {
                    sb.Append(@" AND ""roomType"" = @roomType");
                }
                if (isAvailable.HasValue)
                {
                    sb.Append(@" AND ""isAvailable"" = @isAvailable");
                }
                string commandText = sb.ToString();


                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                if (Id.HasValue)
                {
                    command.Parameters.AddWithValue("@roomId", Id.Value);
                }

                if (!string.IsNullOrEmpty(roomType))
                {
                    command.Parameters.AddWithValue("@roomType", roomType);
                }

                if (isAvailable.HasValue)
                {
                    command.Parameters.AddWithValue("@isAvailable", isAvailable.Value);
                }

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Room room = new Room
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Capacity = reader.GetInt32(2),
                        RoomType = reader.GetString(3),
                        IsAvailable = reader.GetBoolean(4)
                    };
                    rooms.Add(room);
                }

                connection.Close();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("{Id}")]
        public IActionResult GetRoomById(int Id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = "SELECT id, name, capacity, \"roomType\", \"isAvailable\" FROM \"Room\" WHERE id = @roomId";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@roomId", Id);

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                bool hasData = reader.Read();
                if (!hasData)
                {
                    return NotFound();
                }

                Room room = new Room
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Capacity = reader.GetInt32(2),
                    RoomType = reader.GetString(3),
                    IsAvailable = reader.GetBoolean(4)
                };
                connection.Close();
                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpPost]
        public IActionResult PostCreateNewRoom([FromBody] Room newRoom)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText =
                    "INSERT INTO \"Room\" (name, capacity, \"roomType\", \"isAvailable\") VALUES (@name, @capacity, @roomType, @isAvailable);";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@name", newRoom.Name);
                command.Parameters.AddWithValue("@capacity", newRoom.Capacity);
                command.Parameters.AddWithValue("@roomType", newRoom.RoomType);
                command.Parameters.AddWithValue("@isAvailable", newRoom.IsAvailable);

                connection.Open();
                int numberOfRowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsAffected == 0)
                {
                    return BadRequest("Room couldn't be added.");
                }

                return Ok("Room added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{Id}")]
        public IActionResult PutUpdateRoom(int Id, [FromBody] Room updatedRoom)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText =
                     "UPDATE \"Room\" SET capacity = @capacity, \"isAvailable\" = @isAvailable WHERE id = @roomId";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@capacity", updatedRoom.Capacity);
                command.Parameters.AddWithValue("@isAvailable", updatedRoom.IsAvailable);
                command.Parameters.AddWithValue("@roomId", Id);

                connection.Open();
                int numberOfRowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsAffected == 0)
                {
                    return BadRequest("Room couldn't be updated.");
                }

                return Ok("Room successfully updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete("{Id}")]
        public IActionResult DeleteRoom(int Id)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                string commandText = @"DELETE FROM ""Room"" WHERE id = @roomId";
                using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@roomId", Id);

                connection.Open();
                int numberOfRowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfRowsAffected == 0)
                {
                    return BadRequest("Room couldn't be deleted.");
                }
                return Ok("Room successfully deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
