using AssetManagerPro.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace AssetManagerPro.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public List<Category> GetAll()
        {
            List<Category> categories = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, Name, Description FROM Categories ORDER BY Name;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2)
                        ? ""
                        : reader.GetString(2)
                });
            }

            return categories;
        }
    }
}