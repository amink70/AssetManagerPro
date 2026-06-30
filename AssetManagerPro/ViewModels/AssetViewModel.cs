using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using System.Collections.ObjectModel;

namespace AssetManagerPro.ViewModels
{
    public class AssetViewModel
    {
        private readonly IAssetRepository _repository;

        public ObservableCollection<AssetDisplay> Assets { get; }

        public AssetViewModel()
        {
            _repository = new AssetRepository();

            Assets = new ObservableCollection<AssetDisplay>(
                _repository.GetAll());
        }
    }
}