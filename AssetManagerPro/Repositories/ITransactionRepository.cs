using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public interface ITransactionRepository
    {
        List<TransactionDisplay> GetAll();
        List<AssetHistory> GetHistory(int assetId);
        List<AssetTransaction> GetByAsset(int assetId);

        void Add(AssetTransaction transaction);
        void RegisterTransaction(AssetTransaction transaction);
    }
}