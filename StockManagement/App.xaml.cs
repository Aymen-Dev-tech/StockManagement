using StockManagement.Stores;
using StockManagement.ViewModels;
using System.Windows;

namespace StockManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            NavbarViewModel navbarViewModel = new NavbarViewModel(navigationStore);
            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, navbarViewModel);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
