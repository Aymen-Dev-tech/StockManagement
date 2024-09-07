using StockManagement.Commands;
using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockManagement.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {


        public NavbarViewModel NavbarViewModel { get; }

        private ObservableCollection<OrdersProductsCategories> _productsCategories;
        public ObservableCollection<OrdersProductsCategories> ProductsCategories { get { return _productsCategories; } }

        private ObservableCollection<OrdersProductsCategories> _filteredProductsCategories;
        public ObservableCollection<OrdersProductsCategories> FilteredProductsCategories
        {
            get { return _filteredProductsCategories; }
            set { _filteredProductsCategories = value; OnPropertyChanged(nameof(FilteredProductsCategories)); }
        }

        private OrdersProductsCategories _selectedProduct;
        public OrdersProductsCategories SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }
        public ICommand SearchCommand { get; }

        public ICommand CreateOrderCommand { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public OrdersViewModel(NavbarViewModel navbarViewModel)
        {
            NavbarViewModel = navbarViewModel;
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StockManagementDataSource");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(folder, "StockManagementDb.mdf")};Integrated Security=True";
            ProductMapper productsMapper = new ProductMapper(connectionString);
            List<ProductsCategories> fetchedProducts = productsMapper.FindAllProductsCategories();
            _productsCategories = new ObservableCollection<OrdersProductsCategories>();
            foreach (ProductsCategories product in fetchedProducts)
            {
                ProductsCategories.Add(new OrdersProductsCategories(product));
            }
            _filteredProductsCategories = _productsCategories;
            SearchCommand = new OrdersSearchCommand(this);
            CreateOrderCommand = new OrderCommand(this, productsMapper);

        }
        protected override void RestFilteredProducts()

        {
            FilteredProductsCategories = new ObservableCollection<OrdersProductsCategories>(ProductsCategories);
        }

    }
}
