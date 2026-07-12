using AssetManagerPro.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace AssetManagerPro.Views
{
    public partial class ReportsView : UserControl
    {
        private readonly ReportRepository _reportRepository = new();

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


    }
}