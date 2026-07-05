using AssetManagerPro.Models;
using Microsoft.Data.Sqlite;
using System;
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

        public void Add(Category category)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
INSERT INTO Categories
(
    Name,
    Description
)
VALUES
(
    @Name,
    @Description
);";

            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@Description",
                (object?)category.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Update(Category category)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
UPDATE Categories
SET
    Name = @Name,
    Description = @Description
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", category.Id);
            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@Description",
                (object?)category.Description ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
DELETE FROM Categories
WHERE Id = @Id;";

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public Category? GetById(int id)
        {
            throw new NotImplementedException();
        }
        public bool IsUsed(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText =
            @"SELECT COUNT(*)
      FROM Assets
      WHERE CategoryId=@Id;";

            command.Parameters.AddWithValue("@Id", id);

            long count = (long)command.ExecuteScalar()!;

            return count > 0;
        }
    }
}

