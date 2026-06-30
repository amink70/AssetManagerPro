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
using AssetManagerPro.Repositories;
using AssetManagerPro.Models;

namespace AssetManagerPro.Views
{
    /// <summary>
    /// Interaction logic for AddAssetWindow.xaml
    /// </summary>
    public partial class AddAssetWindow : Window
    {
        private readonly AssetRepository _repository = new();

        private Asset? _editingAsset;

        public AddAssetWindow()
        {
            InitializeComponent();
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            IBrandRepository brandRepository = new BrandRepository();
            cmbBrand.ItemsSource = brandRepository.GetAll();

            ICategoryRepository categoryRepository = new CategoryRepository();
            cmbCategory.ItemsSource = categoryRepository.GetAll();

            ISupplierRepository supplierRepository = new SupplierRepository();
            cmbSupplier.ItemsSource = supplierRepository.GetAll();

            ILocationRepository locationRepository = new LocationRepository();
            cmbLocation.ItemsSource = locationRepository.GetAll();

            IReceiverRepository receiverRepository = new ReceiverRepository();
            cmbReceiver.ItemsSource = receiverRepository.GetAll();

            IStatusRepository statusRepository = new StatusRepository();
            cmbStatus.ItemsSource = statusRepository.GetAll();
        }
        public AddAssetWindow(Asset asset)
        {
            InitializeComponent();

            LoadComboBoxes();

            _editingAsset = asset;

            txtAssetCode.Text = asset.AssetCode;
            txtName.Text = asset.Name;

            cmbBrand.SelectedValue = asset.BrandId;
            cmbCategory.SelectedValue = asset.CategoryId;
            cmbSupplier.SelectedValue = asset.SupplierId;
            cmbLocation.SelectedValue = asset.LocationId;
            cmbReceiver.SelectedValue = asset.ReceiverId;
            cmbStatus.SelectedValue = asset.StatusId;

            txtPrice.Text = asset.Price.ToString();
            dpPurchaseDate.SelectedDate = asset.PurchaseDate;
            txtDescription.Text = asset.Description;

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Asset asset = new Asset
            {
                AssetCode = txtAssetCode.Text.Trim(),
                Name = txtName.Text.Trim(),

                BrandId = (int)cmbBrand.SelectedValue,
                CategoryId = (int)cmbCategory.SelectedValue,
                SupplierId = (int)cmbSupplier.SelectedValue,

                LocationId = (int)cmbLocation.SelectedValue,
                ReceiverId = (int)cmbReceiver.SelectedValue,
                StatusId = (int)cmbStatus.SelectedValue,

                Price = string.IsNullOrWhiteSpace(txtPrice.Text)
        ? 0
        : double.Parse(txtPrice.Text),

                PurchaseDate = dpPurchaseDate.SelectedDate ?? DateTime.Now,

                Description = txtDescription.Text.Trim(),

                CreatedAt = DateTime.Now
            };
            AssetRepository repository = new AssetRepository();

            if (_editingAsset == null)
            {
                repository.Add(asset);
                MessageBox.Show("کالا با موفقیت ثبت شد.");
            }
            else
            {
                asset.Id = _editingAsset.Id;

                repository.Update(asset);

                MessageBox.Show("کالا با موفقیت ویرایش شد.");
            }
            txtAssetCode.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            cmbBrand.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            cmbLocation.SelectedIndex = -1;
            cmbReceiver.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            dpPurchaseDate.SelectedDate = DateTime.Today;
            txtAssetCode.Focus();
        }
    }
}
