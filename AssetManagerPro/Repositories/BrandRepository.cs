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
    }
}

