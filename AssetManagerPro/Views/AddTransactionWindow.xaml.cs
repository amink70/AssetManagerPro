using AssetManagerPro.Enums;
using AssetManagerPro.Repositories;
using AssetManagerPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssetManagerPro.Views
{
    /// <summary>
    /// Interaction logic for AddTransactionWindow.xaml
    /// </summary>
    public partial class AddTransactionWindow : Window
    {
        private readonly AssetRepository _assetRepository = new();
        private readonly ReceiverRepository _receiverRepository = new();
        private readonly LocationRepository _locationRepository = new();
        public AddTransactionWindow()
        {
            InitializeComponent();
            cmbTransactionType.ItemsSource = Enum.GetValues(typeof(TransactionType));
            dpTransactionDate.SelectedDate = DateTime.Today;
            LoadAssets();
            LoadReceivers();
            LoadLocations();
        }
        private void LoadAssets()
        {
            cmbAssets.ItemsSource = _assetRepository.GetAll();
            cmbAssets.DisplayMemberPath = "Name";
            cmbAssets.SelectedValuePath = "Id";
        }

        private void LoadReceivers()
        {
            cmbReceivers.ItemsSource = _receiverRepository.GetAll();
            cmbReceivers.DisplayMemberPath = "FullName";
            cmbReceivers.SelectedValuePath = "Id";
        }

        private void LoadLocations()
        {
            cmbLocations.ItemsSource = _locationRepository.GetAll();
            cmbLocations.DisplayMemberPath = "Name";
            cmbLocations.SelectedValuePath = "Id";
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbAssets.SelectedItem == null)
            {
                MessageBox.Show("لطفاً دارایی را انتخاب کنید.");
                return;
            }

            if (cmbTransactionType.SelectedItem == null)
            {
                MessageBox.Show("لطفاً نوع گردش را انتخاب کنید.");
                return;
            }

            if (cmbLocations.SelectedItem == null)
            {
                MessageBox.Show("لطفاً مکان را انتخاب کنید.");
                return;
            }

            if (dpTransactionDate.SelectedDate == null)
            {
                MessageBox.Show("لطفاً تاریخ گردش را انتخاب کنید.");
                return;
            }
            var transactionType = (TransactionType)cmbTransactionType.SelectedItem;

            if (transactionType == TransactionType.Delivery &&
                cmbReceivers.SelectedItem == null)
            {
                MessageBox.Show("برای تحویل، انتخاب گیرنده الزامی است.");
                return;
            }
            var asset = (AssetDisplay)cmbAssets.SelectedItem;

            Receiver? receiver = null;

            if (cmbReceivers.SelectedItem != null)
            {
                receiver = (Receiver)cmbReceivers.SelectedItem;
            }

            var location = (Location)cmbLocations.SelectedItem;

            var transaction = new AssetTransaction
            {
                AssetId = asset.Id,
                ReceiverId = receiver?.Id,
                LocationId = location.Id,
                UserId = 1,
                TransactionType = transactionType,
                TransactionDate = dpTransactionDate.SelectedDate!.Value,
                Description = txtDescription.Text.Trim()
            };

            var repository = new TransactionRepository();

            repository.RegisterTransaction(transaction);

            MessageBox.Show(
                "گردش اموال با موفقیت ثبت شد.",
                "موفق",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DialogResult = true;

            Close();
        }
    }
}
