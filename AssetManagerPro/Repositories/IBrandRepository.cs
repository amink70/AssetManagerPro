using AssetManagerPro.Models;

namespace AssetManagerPro.Repositories
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();

        void Add(Brand brand);

        void Update(Brand brand);

        void Delete(int id);

        Brand? GetById(int id);
    }
}