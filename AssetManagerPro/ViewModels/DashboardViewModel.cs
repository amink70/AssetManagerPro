using AssetManagerPro.Repositories;
using AssetManagerPro.Models;
using System.Collections.ObjectModel;
using System.Globalization;

namespace AssetManagerPro.ViewModels
{
    public class DashboardViewModel
    {
        private readonly DashboardRepository _repository;

        public int TotalAssets { get; set; }
        public int HealthyAssets { get; set; }
        public double HealthyPercent { get; set; }

        public double RepairPercent { get; set; }

        public double BrokenPercent { get; set; }
        public int RepairAssets { get; set; }
        public string TodayDate { get; set; }
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
            HealthyPercent = statistics.HealthyPercent;

            RepairPercent = statistics.RepairPercent;

            BrokenPercent = statistics.BrokenPercent;
            LatestAssets = new ObservableCollection<AssetDisplay>(
    _repository.GetLatestAssets());
            PersianCalendar pc = new PersianCalendar();

            DateTime now = DateTime.Now;

            TodayDate =
                $"{pc.GetYear(now)}/" +
                $"{pc.GetMonth(now):00}/" +
                $"{pc.GetDayOfMonth(now):00}";
        }
    }
}

