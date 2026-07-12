using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagerPro.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AssetManagerPro.ViewModels;

namespace AssetManagerPro.Views
{
    public partial class AssetManagementView : UserControl
    {
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




    }
}
