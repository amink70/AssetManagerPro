using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public List<Status> GetAll()
        {
            List<Status> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, Name, Color, Description FROM Statuses ORDER BY Name;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Status
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Color = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    Description = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }

            return list;
        }
    }
}