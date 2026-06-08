using System.Text;
using Booking.Models;
using Booking.WebApi.Models;
using Npgsql;
using Repository.Common;


namespace Booking.Repository
{
    public class RoomRepository: IRoomRepository
    {

        private string connectionString = "Host=localhost;Port=5432;Database=booking;Username=postgres;Password=jk7pVHZ5";
        public List<Room> GetAllRooms(RoomFilter filter)
        {
            List<Room> rooms = new List<Room>();

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM ""Room"" WHERE 1=1");
            if (filter.Id.HasValue)
            {
                sb.Append(@" AND Id = @roomId");
            }
            if (!string.IsNullOrEmpty(filter.RoomType))
            {
                sb.Append(@" AND ""roomType"" = @roomType");
            }
            if (filter.IsAvailable.HasValue)
            {
                sb.Append(@" AND ""isAvailable"" = @isAvailable");
            }
            string commandText = sb.ToString();


            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            if (filter.Id.HasValue)
            {
                command.Parameters.AddWithValue("@roomId", filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.RoomType))
            {
                command.Parameters.AddWithValue("@roomType", filter.RoomType);
            }
            if (filter.IsAvailable.HasValue)
            {
                command.Parameters.AddWithValue("@isAvailable", filter.IsAvailable.Value);
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
            return (rooms);
        }


        public Room GetRoomById(int id)
        {

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = "SELECT id, name, capacity, \"roomType\", \"isAvailable\" FROM \"Room\" WHERE id = @roomId";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@roomId", id);

            connection.Open();
            using NpgsqlDataReader reader = command.ExecuteReader();

            if (!reader.Read())
            {
                connection.Close();
                return null;
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
            return room;
        }

        public bool CreateNewRoom(Room newRoom)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "INSERT INTO \"Room\" (name, capacity, \"roomType\", \"isAvailable\") VALUES (@name, @capacity, @roomType, @isAvailable);";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@name", newRoom.Name);
            command.Parameters.AddWithValue("@capacity", newRoom.Capacity);
            command.Parameters.AddWithValue("@roomType", newRoom.RoomType);
            command.Parameters.AddWithValue("@isAvailable", newRoom.IsAvailable);

            connection.Open();
            int numberOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();

            return numberOfRowsAffected > 0;
        }

        public bool UpdateRoom(int id, Room updatedRoom)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText =
                "UPDATE \"Room\" SET name = @name, capacity = @capacity, \"roomType\" = @roomType, \"isAvailable\" = @isAvailable WHERE id = @roomId;";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@capacity", updatedRoom.Capacity);
            command.Parameters.AddWithValue("@isAvailable", updatedRoom.IsAvailable);
            command.Parameters.AddWithValue("@roomId", id);

            connection.Open();
            int numberOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();

            return numberOfRowsAffected > 0;
        }

        public bool DeleteRoom(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = "DELETE FROM \"Room\" WHERE id = @roomId;";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@roomId", id);

            connection.Open();
            int numberOfRowsAffected = command.ExecuteNonQuery();
            connection.Close();

            return numberOfRowsAffected > 0;
        }
    }
}
