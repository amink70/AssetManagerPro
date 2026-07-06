using System.Windows;
using System.Windows.Controls;
using AssetManagerPro.Models;
using AssetManagerPro.Repositories;

namespace AssetManagerPro.Views
{
    public partial class SupplierManagementView : UserControl
    {
        private readonly SupplierRepository _repository = new();

        public SupplierManagementView()
        {
            InitializeComponent();

            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            dgSuppliers.ItemsSource = _repository.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSupplierWindow window = new();

            if (window.ShowDialog() == true)
            {
                LoadSuppliers();
            }
        }
        private void dgSuppliers_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgSuppliers.SelectedItem is not Supplier supplier)
                return;

            AddSupplierWindow window = new(supplier);

            if (window.ShowDialog() == true)
            {
                LoadSuppliers();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgSuppliers.SelectedItem is not Supplier supplier)
            {
                MessageBox.Show("ابتدا یک تأمین‌کننده را انتخاب کنید.");
                return;
            }

            if (_repository.IsUsed(supplier.Id))
            {
                MessageBox.Show("این تأمین‌کننده در اموال استفاده شده و قابل حذف نیست.");
                return;
            }

            if (MessageBox.Show(
                "آیا از حذف این تأمین‌کننده مطمئن هستید؟",
                "تأیید حذف",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            _repository.Delete(supplier.Id);

            LoadSuppliers();
        }
    }
}