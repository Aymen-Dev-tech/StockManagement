using StockManagement.Commands;
using StockManagement.DALs;
using StockManagement.Stores;
using System;
using System.IO;
using System.Windows.Input;

namespace StockManagement.ViewModels
{
    public class AddCategoryViewModel : ViewModelBase
    {
        private string _categoryName;
        public string CategoryName 
        { 
            get { return _categoryName; } 
            set { _categoryName = value; OnPropertyChanged(nameof(CategoryName)); } 
        }

        public ICommand SaveCommand { get; }
        public ICommand BackCommand { get; }
        public NavbarViewModel NavbarViewModel { get; }

        public AddCategoryViewModel(NavigationStore navigationStore, NavbarViewModel navbarViewModel)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StockManagementDataSource");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(folder, "StockManagementDb.mdf")};Integrated Security=True";
            SaveCommand = new AddCategoryCommand(this, new CategoryMapper(connectionString));
            BackCommand = new NavigationCommand<AddProduct>(navigationStore, () => new AddProduct(navigationStore, navbarViewModel));
            NavbarViewModel = navbarViewModel;
        }
    }
}
