using StockManagement.Commands;
using StockManagement.Stores;
using System.Windows.Input;

namespace StockManagement.ViewModels
{
    public class NavbarViewModel : ViewModelBase
    {
        public ICommand HomeNavigateCommand { get; }
        public ICommand ProductsNavigateCommand { get; }
        public ICommand OrdersNavigateCommand { get; }
        public ICommand SettingsNavigateCommand { get; }

        public NavbarViewModel(NavigationStore navigationStore)
        {
            HomeNavigateCommand = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore, this));
            ProductsNavigateCommand = new NavigationCommand<AddProduct>(navigationStore, () => new AddProduct(navigationStore, this));
            OrdersNavigateCommand = new NavigationCommand<OrdersViewModel>(navigationStore, () => new OrdersViewModel(this));
            SettingsNavigateCommand = new NavigationCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel(this));
        }
    }
}
