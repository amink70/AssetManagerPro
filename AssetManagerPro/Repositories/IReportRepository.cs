using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public interface IReportRepository
    {
        List<AssetDisplay> GetAllAssetsReport();
        List<AssetDisplay> GetHealthyAssetsReport();
    }
}