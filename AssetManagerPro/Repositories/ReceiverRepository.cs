using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public class ReceiverRepository : BaseRepository, IReceiverRepository
    {
        public List<Receiver> GetAll()
        {
            List<Receiver> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, FullName, PersonnelCode, DepartmentId, Phone, Email, IsActive FROM Receivers ORDER BY FullName;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Receiver
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    PersonnelCode = reader.GetString(2),
                    DepartmentId = reader.GetInt32(3),
                    Phone = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Email = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    IsActive = reader.GetBoolean(6)
                });
            }

            return list;
        }
        public void Add(Receiver receiver)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
INSERT INTO Receivers
(
    FullName,
    PersonnelCode,
    DepartmentId,
    Phone,
    Email,
    IsActive
)
VALUES
(
    @FullName,
    @PersonnelCode,
    @DepartmentId,
    @Phone,
    @Email,
    @IsActive
);";

            command.Parameters.AddWithValue("@FullName", receiver.FullName);
            command.Parameters.AddWithValue("@PersonnelCode", receiver.PersonnelCode);
            command.Parameters.AddWithValue("@DepartmentId", receiver.DepartmentId);
            command.Parameters.AddWithValue("@Phone",
                (object?)receiver.Phone ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email",
                (object?)receiver.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", receiver.IsActive);

            command.ExecuteNonQuery();
        }
        public void Update(Receiver receiver)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
UPDATE Receivers
SET
    FullName = @FullName,
    PersonnelCode = @PersonnelCode,
    DepartmentId = @DepartmentId,
    Phone = @Phone,
    Email = @Email,
    IsActive = @IsActive
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", receiver.Id);
            command.Parameters.AddWithValue("@FullName", receiver.FullName);
            command.Parameters.AddWithValue("@PersonnelCode", receiver.PersonnelCode);
            command.Parameters.AddWithValue("@DepartmentId", receiver.DepartmentId);
            command.Parameters.AddWithValue("@Phone",
                (object?)receiver.Phone ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email",
                (object?)receiver.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@IsActive", receiver.IsActive);

            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
DELETE FROM Receivers
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
        public bool IsUsed(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT COUNT(*)
FROM Assets
WHERE ReceiverId = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            long count = (long)command.ExecuteScalar()!;

            return count > 0;
        }




    }
}