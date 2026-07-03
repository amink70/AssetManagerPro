using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetManagerPro.Views
{
    public partial class BrandManagementView : UserControl
    {
        private readonly BrandRepository _repository = new();

        public BrandManagementView()
        {
            InitializeComponent();

            LoadBrands();
        }

        private void LoadBrands()
        {
            dgBrands.ItemsSource = _repository.GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddBrandWindow window = new();

            if (window.ShowDialog() == true)
            {
                LoadBrands();
            }
        }
        private void dgBrands_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgBrands.SelectedItem == null)
                return;

            Brand brand = (Brand)dgBrands.SelectedItem;

            AddBrandWindow window = new(brand);

            if (window.ShowDialog() == true)
            {
                LoadBrands();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBrands.SelectedItem == null)
            {
                MessageBox.Show("ابتدا یک برند را انتخاب کنید.");
                return;
            }

            Brand brand = (Brand)dgBrands.SelectedItem;

            if (MessageBox.Show(
                $"آیا از حذف برند '{brand.Name}' مطمئن هستید؟",
                "تأیید حذف",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _repository.Delete(brand.Id);

                LoadBrands();

                MessageBox.Show("برند با موفقیت حذف شد.");
            }
        }





    }
}

