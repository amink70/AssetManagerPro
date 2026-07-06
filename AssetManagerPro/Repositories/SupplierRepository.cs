using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        public List<Supplier> GetAll()
        {
            List<Supplier> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, Name, ManagerName, Phone, Email, Address, Description FROM Suppliers ORDER BY Name;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Supplier
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    ManagerName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Email = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Address = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    Description = reader.IsDBNull(6) ? "" : reader.GetString(6)
                });
            }

            return list;
        }
        public void Add(Supplier supplier)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
INSERT INTO Suppliers
(
    Name,
    ManagerName,
    Phone,
    Email,
    Address,
    Description
)
VALUES
(
    @Name,
    @ManagerName,
    @Phone,
    @Email,
    @Address,
    @Description
);";

            command.Parameters.AddWithValue("@Name", supplier.Name);
            command.Parameters.AddWithValue("@ManagerName", (object?)supplier.ManagerName ?? DBNull.Value);
            command.Parameters.AddWithValue("@Phone", (object?)supplier.Phone ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object?)supplier.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Address", (object?)supplier.Address ?? DBNull.Value);
            command.Parameters.AddWithValue("@Description", (object?)supplier.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }
        public void Update(Supplier supplier)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
UPDATE Suppliers
SET
    Name = @Name,
    ManagerName = @ManagerName,
    Phone = @Phone,
    Email = @Email,
    Address = @Address,
    Description = @Description
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", supplier.Id);
            command.Parameters.AddWithValue("@Name", supplier.Name);
            command.Parameters.AddWithValue("@ManagerName", (object?)supplier.ManagerName ?? DBNull.Value);
            command.Parameters.AddWithValue("@Phone", (object?)supplier.Phone ?? DBNull.Value);
            command.Parameters.AddWithValue("@Email", (object?)supplier.Email ?? DBNull.Value);
            command.Parameters.AddWithValue("@Address", (object?)supplier.Address ?? DBNull.Value);
            command.Parameters.AddWithValue("@Description", (object?)supplier.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
DELETE FROM Suppliers
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
WHERE SupplierId = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            long count = (long)command.ExecuteScalar()!;

            return count > 0;
        }
    }
}