using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAll();
    }
}