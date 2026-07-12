using AssetManagerPro.Enums;
using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private readonly IAssetRepository _assetRepository =
    new AssetRepository();
        public void Add(AssetTransaction transaction)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
INSERT INTO AssetTransactions
(
    AssetId,
    ReceiverId,
    LocationId,
    UserId,
    TransactionType,
    TransactionDate,
    Description
)
VALUES
(
    @AssetId,
    @ReceiverId,
    @LocationId,
    @UserId,
    @TransactionType,
    @TransactionDate,
    @Description
);";

            command.Parameters.AddWithValue("@AssetId", transaction.AssetId);

            command.Parameters.AddWithValue(
                "@ReceiverId",
                transaction.ReceiverId.HasValue
                    ? transaction.ReceiverId.Value
                    : DBNull.Value);

            command.Parameters.AddWithValue("@LocationId", transaction.LocationId);
            command.Parameters.AddWithValue("@UserId", transaction.UserId);

            command.Parameters.AddWithValue(
                "@TransactionType",
                (int)transaction.TransactionType);

            command.Parameters.AddWithValue(
                "@TransactionDate",
                transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"));

            command.Parameters.AddWithValue(
                "@Description",
                string.IsNullOrWhiteSpace(transaction.Description)
                    ? DBNull.Value
                    : transaction.Description);

            command.ExecuteNonQuery();
        }



        public List<TransactionDisplay> GetAll()
        {
            List<TransactionDisplay> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT
    t.Id,
    a.Name,
    IFNULL(r.FullName,''),
    l.Name,
    t.TransactionType,
    t.TransactionDate,
    IFNULL(t.Description,'')

FROM AssetTransactions t

INNER JOIN Assets a
ON t.AssetId = a.Id

LEFT JOIN Receivers r
ON t.ReceiverId = r.Id

INNER JOIN Locations l
ON t.LocationId = l.Id

ORDER BY t.Id DESC;";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new TransactionDisplay
                {
                    Id = reader.GetInt32(0),
                    AssetName = reader.GetString(1),
                    ReceiverName = reader.GetString(2),
                    LocationName = reader.GetString(3),
                    TransactionType = ((TransactionType)reader.GetInt32(4)).ToString(),
                    TransactionDate = reader.GetDateTime(5),
                    Description = reader.GetString(6)
                });
            }

            return list;
        }
        public List<AssetTransaction> GetByAsset(int assetId)
        {
            List<AssetTransaction> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT
    Id,
    AssetId,
    ReceiverId,
    LocationId,
    UserId,
    TransactionType,
    TransactionDate,
    Description
FROM AssetTransactions
WHERE AssetId = @AssetId
ORDER BY TransactionDate DESC;";

            command.Parameters.AddWithValue("@AssetId", assetId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AssetTransaction
                {
                    Id = reader.GetInt32(0),
                    AssetId = reader.GetInt32(1),
                    ReceiverId = reader.IsDBNull(2)
                        ? null
                        : reader.GetInt32(2),
                    LocationId = reader.GetInt32(3),
                    UserId = reader.GetInt32(4),

                    TransactionType =
                        (TransactionType)reader.GetInt32(5),

                    TransactionDate =
                        DateTime.Parse(reader.GetString(6)),

                    Description =
                        reader.IsDBNull(7)
                            ? ""
                            : reader.GetString(7)
                });
            }

            return list;
        }
        public void RegisterTransaction(AssetTransaction transaction)
        {
            using var connection = GetConnection();
            connection.Open();

            using var dbTransaction = connection.BeginTransaction();

            try
            {
                // 1- ثبت در AssetTransactions
                using var command = connection.CreateCommand();

                command.Transaction = dbTransaction;

                command.CommandText = @"
INSERT INTO AssetTransactions
(
    AssetId,
    ReceiverId,
    LocationId,
    UserId,
    TransactionType,
    TransactionDate,
    Description
)
VALUES
(
    @AssetId,
    @ReceiverId,
    @LocationId,
    @UserId,
    @TransactionType,
    @TransactionDate,
    @Description
);";

                command.Parameters.AddWithValue("@AssetId", transaction.AssetId);

                command.Parameters.AddWithValue(
                    "@ReceiverId",
                    (object?)transaction.ReceiverId ?? DBNull.Value);

                command.Parameters.AddWithValue("@LocationId", transaction.LocationId);

                command.Parameters.AddWithValue("@UserId", transaction.UserId);

                command.Parameters.AddWithValue(
                    "@TransactionType",
                    (int)transaction.TransactionType);

                command.Parameters.AddWithValue(
                    "@TransactionDate",
                    transaction.TransactionDate);

                command.Parameters.AddWithValue(
                    "@Description",
                    (object?)transaction.Description ?? DBNull.Value);

                command.ExecuteNonQuery();
                int statusId = 1;

                _assetRepository.UpdateAfterTransaction(
                    connection,
                    dbTransaction,
                    transaction.AssetId,
                    transaction.ReceiverId,
                    transaction.LocationId,
                    statusId);
                // 2- بروزرسانی Assets

                dbTransaction.Commit();
            }
            catch
            {
                dbTransaction.Rollback();
                throw;
            }
        }
        public List<AssetHistory> GetHistory(int assetId)
        {
            List<AssetHistory> list = new();

            using var connection = GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = @"
SELECT
    t.TransactionDate,
    t.TransactionType,
    IFNULL(r.FullName,''),
    l.Name,
    u.FullName,
    IFNULL(t.Description,'')

FROM AssetTransactions t

LEFT JOIN Receivers r
ON t.ReceiverId = r.Id

INNER JOIN Locations l
ON t.LocationId = l.Id

INNER JOIN Users u
ON t.UserId = u.Id

WHERE t.AssetId = @AssetId

ORDER BY t.TransactionDate DESC;";

            command.Parameters.AddWithValue("@AssetId", assetId);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new AssetHistory
                {
                    TransactionDate = reader.GetDateTime(0),

                    TransactionType =
                        ((TransactionType)reader.GetInt32(1)).ToString(),

                    ReceiverName = reader.GetString(2),

                    LocationName = reader.GetString(3),

                    UserName = reader.GetString(4),

                    Description = reader.GetString(5)
                });
            }

            return list;
        }
    }
}

