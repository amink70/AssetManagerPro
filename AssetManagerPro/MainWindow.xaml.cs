using System.Windows;
using AssetManagerPro.Views;

namespace AssetManagerPro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainContent.Content = new DashboardView();
        }

        private void btnAssets_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new AssetManagementView();
        }

        private void btnBrands_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BrandManagementView();
        }
        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TransactionManagementView();
        }
        private void btnCategories_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CategoryManagementView();
        }
        private void btnSuppliers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SupplierManagementView();
        }
        private void btnLocations_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new LocationManagementView();
        }
        private void btnReceivers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ReceiverManagementView();
        }
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DashboardView();
        }
        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ReportsView();
        }
    }
}