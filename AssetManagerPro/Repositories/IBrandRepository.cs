using AssetManagerPro.Models;
using System.Collections.Generic;

namespace AssetManagerPro.Repositories
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
    }
}