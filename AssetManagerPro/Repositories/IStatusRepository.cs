using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public interface IStatusRepository
    {
        List<Status> GetAll();
    }
}