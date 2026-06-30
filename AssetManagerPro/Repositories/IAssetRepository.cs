using AssetManagerPro.Models;
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
        
    }
}

