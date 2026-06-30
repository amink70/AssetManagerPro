using AssetManagerPro.Repositories;
using AssetManagerPro.Models;
using System.Collections.ObjectModel;

namespace AssetManagerPro.ViewModels
{
    public class DashboardViewModel
    {
        private readonly DashboardRepository _repository;

        public int TotalAssets { get; set; }
        public int HealthyAssets { get; set; }

        public int RepairAssets { get; set; }

        public int BrokenAssets { get; set; }
        public ObservableCollection<AssetDisplay> LatestAssets { get; set; }

        public DashboardViewModel()
        {
            _repository = new DashboardRepository();

            DashboardStatistics statistics = _repository.GetStatistics();

            TotalAssets = statistics.TotalAssets;
            HealthyAssets = statistics.HealthyAssets;
            RepairAssets = statistics.RepairAssets;
            BrokenAssets = statistics.BrokenAssets;

            LatestAssets = new ObservableCollection<AssetDisplay>(
    _repository.GetLatestAssets());
        }
    }
}

