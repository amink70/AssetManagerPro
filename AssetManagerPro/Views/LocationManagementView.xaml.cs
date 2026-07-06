using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetManagerPro.Views
{
    public partial class LocationManagementView : UserControl
    {
        private readonly LocationRepository _repository = new();

        public LocationManagementView()
        {
            InitializeComponent();

            LoadLocations();
        }

        private void LoadLocations()
        {
            dgLocations.ItemsSource = _repository.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLocationWindow window = new();

            if (window.ShowDialog() == true)
            {
                LoadLocations();
            }
        }

        private void dgLocations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgLocations.SelectedItem is not Location location)
                return;

            AddLocationWindow window = new(location);

            if (window.ShowDialog() == true)
            {
                LoadLocations();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgLocations.SelectedItem is not Location location)
            {
                MessageBox.Show("ابتدا یک مکان را انتخاب کنید.");
                return;
            }

            if (_repository.IsUsed(location.Id))
            {
                MessageBox.Show("این مکان در اموال استفاده شده و قابل حذف نیست.");
                return;
            }

            if (MessageBox.Show(
                "آیا از حذف این مکان مطمئن هستید؟",
                "تأیید حذف",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            _repository.Delete(location.Id);

            LoadLocations();
        }


    }
}