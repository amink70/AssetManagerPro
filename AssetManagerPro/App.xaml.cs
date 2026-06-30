using System.Windows;
using AssetManagerPro.Database;

namespace AssetManagerPro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DatabaseManager.Initialize();
        }
    }
}