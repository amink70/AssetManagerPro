using AssetManagerPro.Models;
using System.Collections.Generic;

namespace AssetManagerPro.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}