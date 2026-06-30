using AssetManagerPro.Models;
using AssetManagerPro.Database;
using Microsoft.Data.Sqlite;


namespace AssetManagerPro.Repositories
{
    public class AssetRepository :BaseRepository, IAssetRepository
    {
        
        public void Add(Asset asset)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = @"
INSERT INTO Assets
(
    AssetCode,
    Name,
    BrandId,
    CategoryId,
    SupplierId,
    
    LocationId,
    ReceiverId,
    StatusId,
    Price,
    PurchaseDate,
    
    Description,
    CreatedAt
    
)
VALUES
(
    @AssetCode,
    @Name,
    @BrandId,
    @CategoryId,
    @SupplierId,
   
    
    @LocationId,
    @ReceiverId,
    @StatusId,
    @Price,
    @PurchaseDate,
    
    @Description,
    @CreatedAt
    
);";
            command.Parameters.AddWithValue("@AssetCode", asset.AssetCode);
            command.Parameters.AddWithValue("@Name", asset.Name);
            command.Parameters.AddWithValue("@BrandId", asset.BrandId);
            command.Parameters.AddWithValue("@CategoryId", asset.CategoryId);
            command.Parameters.AddWithValue("@SupplierId", (object?)asset.SupplierId ?? DBNull.Value);
         
          
            command.Parameters.AddWithValue("@LocationId", asset.LocationId);
            command.Parameters.AddWithValue("@ReceiverId", (object?)asset.ReceiverId ?? DBNull.Value);
            command.Parameters.AddWithValue("@StatusId", asset.StatusId);
            command.Parameters.AddWithValue("@Price", asset.Price);
            command.Parameters.AddWithValue("@PurchaseDate", (object?)asset.PurchaseDate ?? DBNull.Value);
           
            command.Parameters.AddWithValue("@Description", (object?)asset.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@CreatedAt", asset.CreatedAt);
         
            command.ExecuteNonQuery();
        }

        public void Update(Asset asset)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
UPDATE Assets
SET
    AssetCode = @AssetCode,
    Name = @Name,
    BrandId = @BrandId,
    CategoryId = @CategoryId,
    SupplierId = @SupplierId,
    LocationId = @LocationId,
    ReceiverId = @ReceiverId,
    StatusId = @StatusId,
    Price = @Price,
    PurchaseDate = @PurchaseDate,
    Description = @Description,
    UpdatedAt = @UpdatedAt
WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Id", asset.Id);

            command.Parameters.AddWithValue("@AssetCode", asset.AssetCode);
            command.Parameters.AddWithValue("@Name", asset.Name);
            command.Parameters.AddWithValue("@BrandId", asset.BrandId);
            command.Parameters.AddWithValue("@CategoryId", asset.CategoryId);
            command.Parameters.AddWithValue("@SupplierId", (object?)asset.SupplierId ?? DBNull.Value);
            command.Parameters.AddWithValue("@LocationId", asset.LocationId);
            command.Parameters.AddWithValue("@ReceiverId", (object?)asset.ReceiverId ?? DBNull.Value);
            command.Parameters.AddWithValue("@StatusId", asset.StatusId);
            command.Parameters.AddWithValue("@Price", asset.Price);
            command.Parameters.AddWithValue("@PurchaseDate", (object?)asset.PurchaseDate ?? DBNull.Value);
            command.Parameters.AddWithValue("@Description", (object?)asset.Description ?? DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            command.ExecuteNonQuery();

        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
DELETE FROM Assets
WHERE Id = @Id";

            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public Asset? GetById(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
SELECT
Id,
AssetCode,
Name,
BrandId,
CategoryId,
SupplierId,
LocationId,
ReceiverId,
StatusId,
Price,
PurchaseDate,
Description,
CreatedAt,
UpdatedAt
FROM Assets
WHERE Id = @Id;";
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Asset
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),

                    BrandId = reader.GetInt32(3),
                    CategoryId = reader.GetInt32(4),
                    SupplierId = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                    LocationId = reader.GetInt32(6),
                    ReceiverId = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                    StatusId = reader.GetInt32(8),

                    Price = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),

                    PurchaseDate = reader.IsDBNull(10)
                        ? null
                        : reader.GetDateTime(10),

                    Description = reader.IsDBNull(11)
                        ? ""
                        : reader.GetString(11),

                    CreatedAt = reader.GetDateTime(12),

                    UpdatedAt = reader.IsDBNull(13)
                        ? null
                        : reader.GetDateTime(13)
                };
            }

            return null;

        }


        public List<AssetDisplay> GetAll()
        {
            List<AssetDisplay> assets = new();

            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
SELECT
    A.Id,
    A.AssetCode,
    A.Name,
    B.Name AS Brand,
    C.Name AS Category,
    A.Model,
    A.SerialNumber,
    L.Name AS Location,
    IFNULL(R.FullName, '') AS Receiver,
    S.Name AS Status,
    A.Price,
    A.PurchaseDate
FROM Assets A

INNER JOIN Brands B
    ON A.BrandId = B.Id

INNER JOIN Categories C
    ON A.CategoryId = C.Id

INNER JOIN Locations L
    ON A.LocationId = L.Id

LEFT JOIN Receivers R
    ON A.ReceiverId = R.Id

INNER JOIN Statuses S
    ON A.StatusId = S.Id

ORDER BY A.Id DESC;";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                AssetDisplay asset = new()
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),
                    Brand = reader.GetString(3),
                    Category = reader.GetString(4),
                    Model = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    SerialNumber = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Location = reader.GetString(7),
                    Receiver = reader.GetString(8),
                    Status = reader.GetString(9),
                    Price = reader.GetDouble(10),
                    PurchaseDate = reader.IsDBNull(11)
        ? null
        : DateTime.Parse(reader.GetString(11))
            
                };
                assets.Add(asset);
            }
            return assets;
        }
    }
}