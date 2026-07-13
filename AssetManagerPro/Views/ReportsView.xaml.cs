using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using AssetManagerPro.Services;
using System.Windows;
using System.Windows.Controls;

namespace AssetManagerPro.Views
{
    public partial class ReportsView : UserControl
    {
        private readonly ReportRepository _reportRepository = new();
        private readonly ReportRepository _repository = new();
        private readonly ExcelService _excelService = new();
        public ReportsView()
        {
            InitializeComponent();
        }

        private void btnAllAssets_Click(object sender, RoutedEventArgs e)
        {
            dgReports.ItemsSource = _reportRepository.GetAllAssetsReport();
        }
        private void btnHealthyAssets_Click(object sender, RoutedEventArgs e)
        {
            ReportRepository repository = new();

            dgReports.ItemsSource = repository.GetHealthyAssetsReport();
        }
        private void btnBrokenAssets_Click(object sender, RoutedEventArgs e)
        {
            ReportRepository repository = new();

            dgReports.ItemsSource = repository.GetBrokenAssetsReport();
        }
        private void btnRepairAssets_Click(object sender, RoutedEventArgs e)
        {
            ReportRepository repository = new();

            dgReports.ItemsSource = repository.GetRepairAssetsReport();
        }
        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            var assets = dgReports.ItemsSource as List<AssetDisplay>;

            if (assets == null || assets.Count == 0)
            {
                MessageBox.Show(
                    "ابتدا یک گزارش را نمایش دهید.",
                    "خروجی Excel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            _excelService.ExportAssets(assets);
        }


    }
}