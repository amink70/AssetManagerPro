using AssetManagerPro.Models;
using AssetManagerPro.Repositories;
using AssetManagerPro.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssetManagerPro.Views
{
    public partial class AssetManagementView : UserControl
    {
        private readonly AssetRepository _repository = new();
        public AssetManagementView()
        {
            InitializeComponent();

            DataContext = new AssetManagementViewModel();
        }
        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgAssets.SelectedItem is not AssetDisplay asset)
            {
                MessageBox.Show(
                    "لطفاً ابتدا یک دارایی را انتخاب کنید.",
                    "تاریخچه دارایی",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            AssetHistoryWindow window = new(asset.Id);

            window.Owner = Window.GetWindow(this);

            window.ShowDialog();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddAssetWindow window = new();

            window.Owner = Window.GetWindow(this);

            window.AssetSaved += () =>
            {
                ((AssetManagementViewModel)DataContext).LoadAssets();
            };

            window.ShowDialog();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgAssets.SelectedItem is not AssetDisplay selectedAsset)
            {
                MessageBox.Show(
                    "لطفاً ابتدا یک دارایی را انتخاب کنید.",
                    "ویرایش",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            Asset asset = _repository.GetById(selectedAsset.Id);

            AddAssetWindow window = new(asset);

            window.Owner = Window.GetWindow(this);

            window.AssetSaved += () =>
            {
                ((AssetManagementViewModel)DataContext).LoadAssets();
            };

            window.ShowDialog();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAssets.SelectedItem is not AssetDisplay selectedAsset)
            {
                MessageBox.Show(
                    "لطفاً ابتدا یک دارایی را انتخاب کنید.",
                    "حذف دارایی",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            MessageBoxResult result = MessageBox.Show(
                $"آیا از حذف دارایی «{selectedAsset.Name}» مطمئن هستید؟",
                "تأیید حذف",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            _repository.Delete(selectedAsset.Id);

            ((AssetManagementViewModel)DataContext).LoadAssets();

            MessageBox.Show(
                "دارایی با موفقیت حذف شد.",
                "حذف",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }



    }
}
