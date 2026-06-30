using AssetManagerPro.Database;
using Microsoft.Data.Sqlite;

namespace AssetManagerPro.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly string ConnectionString =
            DatabaseManager.ConnectionString;

        protected SqliteConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}