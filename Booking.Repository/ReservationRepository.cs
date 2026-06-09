using Booking.Models;
using Npgsql;
using System.Text;
using BookingRepository.Common;
using Booking.Common;


namespace Booking.Repository
{
    public class ReservationRepository: IReservationRepository
    {
        private string connectionString = "Host=localhost;Port=5432;Database=booking;Username=postgres;Password=jk7pVHZ5";

        public async Task <List<Reservation>> GetAllReservationsAsync(ReservationFilter filter)
        {
            List<Reservation> reservations = new List<Reservation>();

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            StringBuilder sb = new StringBuilder();

            sb.Append(@"SELECT * FROM ""Reservation"" WHERE 1 = 1");
            if (filter.Id.HasValue)
            {
                sb.Append(@" AND Id = @reservationId");
            }
            if (filter.RoomId.HasValue)
            {
                sb.Append(@"AND roomId = @roomId");
            }
            if (!string.IsNullOrEmpty(filter.FullName))
            {
                sb.Append(@"AND fullName = @fullName");
            }
            if (filter.IsAvailable.HasValue)
            {
                sb.Append(@"AND isAvailable = @isAvailable");
            }

            string commandText = sb.ToString();
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            if (filter.Id.HasValue)
            {
                command.Parameters.AddWithValue("@reservationId", filter.Id.Value);
            }
            if (filter.RoomId.HasValue)
            {
                command.Parameters.AddWithValue("@roomId", filter.RoomId.Value);
            }
            if (!string.IsNullOrEmpty(filter.FullName))
            {
                command.Parameters.AddWithValue("@fullName", filter.FullName);
            }
            if (filter.IsAvailable.HasValue)
            {
                command.Parameters.AddWithValue("@isAvailable", filter.IsAvailable.Value);
            }

            connection.Open();
            using NpgsqlDataReader reader =await command.ExecuteReaderAsync();

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
            return reservations;
        }

        public async Task <Reservation> GetReservationByIdAsync(int reservationId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = @"SELECT * FROM ""Reservation"" WHERE Id = @reservationId";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@reservationId", reservationId);

            connection.Open();
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
            if (!reader.Read())
            {
                connection.Close();
                return null;
            }

            Reservation reservation = new Reservation
            {
                Id = reader.GetInt32(0),
                RoomId = reader.GetInt32(1),
                FullName = reader.GetString(2),
                ReservationDate = reader.GetDateTime(3),
                IsAvailable = reader.GetBoolean(4)
            };

            connection.Close();
            return reservation;
        }


        public async Task <bool> CreateNewReservation(Reservation newReservation)
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
            int numberOfRowsAffected = await command.ExecuteNonQueryAsync();
            connection.Close();

            return numberOfRowsAffected > 0;
        }

        public async Task <bool> UpdateReservation(int reservationId, Reservation updatedReservation)
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
            int numberOfRowsAffected = await command.ExecuteNonQueryAsync();
            connection.Close();

            return numberOfRowsAffected > 0;
        }

        public async Task <bool> DeleteReservation(int reservationId)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string commandText = $"DELETE FROM \"Reservation\" WHERE Id = @reservationId";
            using NpgsqlCommand command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@reservationId", reservationId);

            connection.Open();
            int numberOfRowsAffected = await command.ExecuteNonQueryAsync();
            connection.Close();

            return numberOfRowsAffected > 0;
        }

    }
}
