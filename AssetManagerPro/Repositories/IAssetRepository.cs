using AssetManagerPro.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace AssetManagerPro.Repositories
{
    public interface IAssetRepository
    {
        void Add(Asset asset);

        void Update(Asset asset);

        void Delete(int id);

        Asset? GetById(int id);

        List<AssetDisplay> GetAll();
        void UpdateAfterTransaction(
    SqliteConnection connection,
    SqliteTransaction transaction,
    int assetId,
    int? receiverId,
    int locationId,
    int statusId);
    }
}

