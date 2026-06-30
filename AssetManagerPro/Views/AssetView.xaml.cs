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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssetManagerPro.Views
{
    /// <summary>
    /// Interaction logic for AssetView.xaml
    /// </summary>
    public partial class AssetView : UserControl
    {
        
        public AssetView()
        {
            InitializeComponent();
            DataContext = new AssetViewModel();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddAssetWindow();
            window.ShowDialog();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgAssets.SelectedItem == null)
            {
                MessageBox.Show("لطفاً ابتدا یک کالا را انتخاب کنید.");
                return;
            }

            AssetDisplay selectedAsset = (AssetDisplay)dgAssets.SelectedItem;
            AssetRepository repository = new AssetRepository();

            Asset? asset = repository.GetById(selectedAsset.Id);

            if (asset == null)
            {
                MessageBox.Show("اطلاعات کالا پیدا نشد.");
                return;
            }
            var window = new AddAssetWindow(asset);

            window.ShowDialog();
        }
    }
}
