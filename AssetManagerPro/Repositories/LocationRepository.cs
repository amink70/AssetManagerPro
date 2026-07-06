using AssetManagerPro.Models;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AssetManagerPro.Repositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public List<Location> GetAll()
        {
            List<Location> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT\r\n    l.Id,\r\n    l.DepartmentId,\r\n    d.Name,\r\n    l.Name,\r\n    l.Description\r\nFROM Locations l\r\nINNER JOIN Departments d\r\nON l.DepartmentId = d.Id\r\nORDER BY l.Name";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Location
                {
                    Id = reader.GetInt32(0),
                    DepartmentId = reader.GetInt32(1),
                    DepartmentName = reader.GetString(2),
                    Name = reader.GetString(3),
                    Description = reader.IsDBNull(4) ? "" : reader.GetString(4)
                });
            }

            return list;
        }
        public void Add(Location location)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
INSERT INTO Locations
(
    DepartmentId,
    Name,
    Description
)
VALUES
(
    @DepartmentId,
    @Name,
    @Description
);";

            command.Parameters.AddWithValue("@DepartmentId", location.DepartmentId);
            command.Parameters.AddWithValue("@Name", location.Name);
            command.Parameters.AddWithValue("@Description",
                (object?)location.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Update(Location location)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
UPDATE Locations
SET
    DepartmentId = @DepartmentId,
    Name = @Name,
    Description = @Description
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", location.Id);
            command.Parameters.AddWithValue("@DepartmentId", location.DepartmentId);
            command.Parameters.AddWithValue("@Name", location.Name);
            command.Parameters.AddWithValue("@Description",
                (object?)location.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
DELETE FROM Locations
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
WHERE LocationId = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            long count = (long)command.ExecuteScalar()!;

            return count > 0;
        }


    }
}