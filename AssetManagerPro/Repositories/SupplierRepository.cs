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
    }
}