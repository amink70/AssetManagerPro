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
using AssetManagerPro.Repositories;
using AssetManagerPro.Models;

namespace AssetManagerPro.Views
{
    /// <summary>
    /// Interaction logic for TransactionManagementView.xaml
    /// </summary>
    public partial class TransactionManagementView : UserControl
    {
        private readonly ITransactionRepository _transactionRepository =
    new TransactionRepository();
        public TransactionManagementView()
        {
            InitializeComponent();

            LoadTransactions();
        }
        private void LoadTransactions()
        {
            dgTransactions.ItemsSource = _transactionRepository.GetAll();
        }
        private void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddTransactionWindow();

            bool? result = window.ShowDialog();

            if (result == true)
            {
                LoadTransactions();
            }
        }
    }
}
