using AssetManagerPro.Repositories;
using System.Windows;

namespace AssetManagerPro.Views
{
    public partial class AssetHistoryWindow : Window
    {
        private readonly TransactionRepository _repository = new();

        public AssetHistoryWindow(int assetId)
        {
            InitializeComponent();

            dgHistory.ItemsSource = _repository.GetHistory(assetId);
        }
    }
}