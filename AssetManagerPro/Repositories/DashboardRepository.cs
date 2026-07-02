using AssetManagerPro.Models;
using Microsoft.Data.Sqlite;

namespace AssetManagerPro.Repositories
{
    public class DashboardRepository : BaseRepository
    {
        
        
        public List<AssetDisplay> GetLatestAssets()
        {
            List<AssetDisplay> assets = new();

            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT
    Assets.Id,
    Assets.AssetCode,
    Assets.Name,
    Brands.Name,
    Assets.Model,
    Statuses.Name,
    Locations.Name,
    Receivers.FullName
FROM Assets
LEFT JOIN Brands ON Assets.BrandId = Brands.Id
LEFT JOIN Statuses ON Assets.StatusId = Statuses.Id
LEFT JOIN Locations ON Assets.LocationId = Locations.Id
LEFT JOIN Receivers ON Assets.ReceiverId = Receivers.Id
ORDER BY Assets.Id DESC
LIMIT 5;
";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                assets.Add(new AssetDisplay
                {
                    Id = reader.GetInt32(0),
                    AssetCode = reader.GetString(1),
                    Name = reader.GetString(2),
                    Brand = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Model = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Status = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    Location = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Receiver = reader.IsDBNull(7) ? "" : reader.GetString(7)
                });
            }

            return assets;
        }
        public DashboardStatistics GetStatistics()
        {
            using var connection = new SqliteConnection(ConnectionString);

            connection.Open();

            DashboardStatistics statistics = new();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Assets";
                statistics.TotalAssets = Convert.ToInt32(command.ExecuteScalar());
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT COUNT(*)
FROM Assets
WHERE StatusId = (
    SELECT Id FROM Statuses WHERE Name='سالم'
)";
                statistics.HealthyAssets = Convert.ToInt32(command.ExecuteScalar());
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT COUNT(*)
FROM Assets
WHERE StatusId = (
    SELECT Id FROM Statuses WHERE Name='در تعمیر'
)";
                statistics.RepairAssets = Convert.ToInt32(command.ExecuteScalar());
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
SELECT COUNT(*)
FROM Assets
WHERE StatusId = (
    SELECT Id FROM Statuses WHERE Name='خراب'
)";
                statistics.BrokenAssets = Convert.ToInt32(command.ExecuteScalar());
            }

            if (statistics.TotalAssets > 0)
            {
                statistics.HealthyPercent =
                    statistics.HealthyAssets * 100.0 / statistics.TotalAssets;

                statistics.RepairPercent =
                    statistics.RepairAssets * 100.0 / statistics.TotalAssets;

                statistics.BrokenPercent =
                    statistics.BrokenAssets * 100.0 / statistics.TotalAssets;
            }
            return statistics;
        }


    }
}

