using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public class DepartmentRepository : BaseRepository
    {
        public List<Department> GetAll()
        {
            List<Department> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, Name, Description FROM Departments ORDER BY Name;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Department
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2)
                        ? ""
                        : reader.GetString(2)
                });
            }

            return list;
        }
    }
}

