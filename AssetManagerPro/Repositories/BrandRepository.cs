using AssetManagerPro.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace AssetManagerPro.Repositories
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {
        public List<Brand> GetAll()
        {
            List<Brand> brands = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, Name, Description FROM Brands ORDER BY Name;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                brands.Add(new Brand
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2)
                        ? ""
                        : reader.GetString(2)
                });
            }

            return brands;
        }
        public void Add(Brand brand)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
INSERT INTO Brands
(
    Name,
    Description
)
VALUES
(
    @Name,
    @Description
);";

            command.Parameters.AddWithValue("@Name", brand.Name);
            command.Parameters.AddWithValue("@Description",
                (object?)brand.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }
        public void Update(Brand brand)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
UPDATE Brands
SET
    Name = @Name,
    Description = @Description
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", brand.Id);
            command.Parameters.AddWithValue("@Name", brand.Name);
            command.Parameters.AddWithValue("@Description",
                (object?)brand.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
DELETE FROM Brands
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }


        public Brand? GetById(int id)
        {
            throw new NotImplementedException();
        }

    }
}

