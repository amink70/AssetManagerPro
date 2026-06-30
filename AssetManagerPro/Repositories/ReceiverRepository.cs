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
    }
}