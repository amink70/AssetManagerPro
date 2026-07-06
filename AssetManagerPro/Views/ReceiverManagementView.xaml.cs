using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AssetManagerPro.Views
{
    public partial class ReceiverManagementView : UserControl
    {
        private readonly ReceiverRepository _repository = new();

        public ReceiverManagementView()
        {
            InitializeComponent();

            LoadReceivers();
        }

        private void LoadReceivers()
        {
            dgReceivers.ItemsSource = _repository.GetAll();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddReceiverWindow window = new();

            if (window.ShowDialog() == true)
            {
                LoadReceivers();
            }
        }

        private void dgReceivers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgReceivers.SelectedItem is not Receiver receiver)
                return;

            AddReceiverWindow window = new(receiver);

            if (window.ShowDialog() == true)
            {
                LoadReceivers();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgReceivers.SelectedItem is not Receiver receiver)
            {
                MessageBox.Show("ابتدا یک گیرنده را انتخاب کنید.");
                return;
            }

            if (_repository.IsUsed(receiver.Id))
            {
                MessageBox.Show("این گیرنده در اموال استفاده شده و قابل حذف نیست.");
                return;
            }

            if (MessageBox.Show(
                "آیا از حذف این گیرنده مطمئن هستید؟",
                "تأیید حذف",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            _repository.Delete(receiver.Id);

            LoadReceivers();
        }
    }
}

