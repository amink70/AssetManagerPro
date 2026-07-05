using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AssetManagerPro.Models;
using AssetManagerPro.Repositories;

namespace AssetManagerPro.Views
{
    public partial class CategoryManagementView : UserControl
    {
        private readonly CategoryRepository _repository = new();

        public CategoryManagementView()
        {
            InitializeComponent();

            LoadCategories();
        }

        private void LoadCategories()
        {
            dgCategories.ItemsSource = null;
            dgCategories.ItemsSource = _repository.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow window = new();

            if (window.ShowDialog() == true)
            {
                LoadCategories();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgCategories.SelectedItem == null)
            {
                MessageBox.Show("ابتدا یک دسته‌بندی را انتخاب کنید.");
                return;
            }

            Category category = (Category)dgCategories.SelectedItem;

            if (MessageBox.Show(
                $"آیا از حذف '{category.Name}' مطمئن هستید؟",
                "تأیید حذف",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (_repository.IsUsed(category.Id))
                {
                    MessageBox.Show(
                        "این دسته‌بندی در اموال استفاده شده و قابل حذف نیست.");

                    return;
                }
                _repository.Delete(category.Id);

                LoadCategories();

                MessageBox.Show("دسته‌بندی با موفقیت حذف شد.");
            }
        }

        private void dgCategories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgCategories.SelectedItem == null)
                return;

            Category category = (Category)dgCategories.SelectedItem;

            AddCategoryWindow window = new(category);

            if (window.ShowDialog() == true)
            {
                LoadCategories();
            }
        }
    }
}