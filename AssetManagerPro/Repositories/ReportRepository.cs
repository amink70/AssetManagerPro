using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public List<AssetDisplay> GetAllAssetsReport()
        {
            List<AssetDisplay> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT

A.Id,
A.AssetCode,
A.Name,

B.Name AS Brand,

C.Name AS Category,

IFNULL(L.Name,'') AS Location,

IFNULL(R.FullName,'') AS Receiver,

S.Name AS Status,

A.Price,

A.PurchaseDate

FROM Assets A

INNER JOIN Brands B
ON A.BrandId = B.Id

INNER JOIN Categories C
ON A.CategoryId = C.Id

LEFT JOIN Locations L
ON A.LocationId = L.Id

LEFT JOIN Receivers R
ON A.ReceiverId = R.Id

INNER JOIN Statuses S
ON A.StatusId = S.Id

ORDER BY A.AssetCode;
";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AssetDisplay
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),
                    Brand = reader.GetString(3),
                    Category = reader.GetString(4),
                    Location = reader.GetString(5),
                    Receiver = reader.GetString(6),
                    Status = reader.GetString(7),
                    Price = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                    PurchaseDate = reader.IsDBNull(9)
                        ? null
                        : reader.GetDateTime(9)
                });
            }

            return list;
        }
        public List<AssetDisplay> GetHealthyAssetsReport()
        {
            List<AssetDisplay> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT

A.Id,
A.AssetCode,
A.Name,

B.Name AS Brand,

C.Name AS Category,

IFNULL(L.Name,'') AS Location,

IFNULL(R.FullName,'') AS Receiver,

S.Name AS Status,

A.Price,

A.PurchaseDate

FROM Assets A

INNER JOIN Brands B
ON A.BrandId = B.Id

INNER JOIN Categories C
ON A.CategoryId = C.Id

LEFT JOIN Locations L
ON A.LocationId = L.Id

LEFT JOIN Receivers R
ON A.ReceiverId = R.Id

INNER JOIN Statuses S
ON A.StatusId = S.Id

WHERE S.Name = 'سالم'

ORDER BY A.AssetCode;
";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AssetDisplay
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),
                    Brand = reader.GetString(3),
                    Category = reader.GetString(4),
                    Location = reader.GetString(5),
                    Receiver = reader.GetString(6),
                    Status = reader.GetString(7),
                    Price = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                    PurchaseDate = reader.IsDBNull(9)
                        ? null
                        : reader.GetDateTime(9)
                });
            }

            return list;
        }
        public List<AssetDisplay> GetBrokenAssetsReport()
        {
            List<AssetDisplay> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT

A.Id,
A.AssetCode,
A.Name,

B.Name AS Brand,

C.Name AS Category,

IFNULL(L.Name,'') AS Location,

IFNULL(R.FullName,'') AS Receiver,

S.Name AS Status,

A.Price,

A.PurchaseDate

FROM Assets A

INNER JOIN Brands B
ON A.BrandId = B.Id

INNER JOIN Categories C
ON A.CategoryId = C.Id

LEFT JOIN Locations L
ON A.LocationId = L.Id

LEFT JOIN Receivers R
ON A.ReceiverId = R.Id

INNER JOIN Statuses S
ON A.StatusId = S.Id

WHERE S.Name = 'خراب'

ORDER BY A.AssetCode;
";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AssetDisplay
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),
                    Brand = reader.GetString(3),
                    Category = reader.GetString(4),
                    Location = reader.GetString(5),
                    Receiver = reader.GetString(6),
                    Status = reader.GetString(7),
                    Price = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                    PurchaseDate = reader.IsDBNull(9)
                        ? null
                        : reader.GetDateTime(9)
                });
            }

            return list;
        }
        public List<AssetDisplay> GetRepairAssetsReport()
        {
            List<AssetDisplay> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT

A.Id,
A.AssetCode,
A.Name,

B.Name AS Brand,

C.Name AS Category,

IFNULL(L.Name,'') AS Location,

IFNULL(R.FullName,'') AS Receiver,

S.Name AS Status,

A.Price,

A.PurchaseDate

FROM Assets A

INNER JOIN Brands B
ON A.BrandId = B.Id

INNER JOIN Categories C
ON A.CategoryId = C.Id

LEFT JOIN Locations L
ON A.LocationId = L.Id

LEFT JOIN Receivers R
ON A.ReceiverId = R.Id

INNER JOIN Statuses S
ON A.StatusId = S.Id

WHERE S.Name = 'در تعمیر'

ORDER BY A.AssetCode;
";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AssetDisplay
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),
                    Brand = reader.GetString(3),
                    Category = reader.GetString(4),
                    Location = reader.GetString(5),
                    Receiver = reader.GetString(6),
                    Status = reader.GetString(7),
                    Price = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                    PurchaseDate = reader.IsDBNull(9)
                        ? null
                        : reader.GetDateTime(9)
                });
            }

            return list;
        }

    }
}

