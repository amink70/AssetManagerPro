using AssetManagerPro.Models;

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
                "SELECT Id, DepartmentId, Name, Description FROM Locations ORDER BY Name;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Location
                {
                    Id = reader.GetInt32(0),
                    DepartmentId = reader.GetInt32(1),
                    Name = reader.GetString(2),
                    Description = reader.IsDBNull(3) ? "" : reader.GetString(3)
                });
            }

            return list;
        }
    }
}