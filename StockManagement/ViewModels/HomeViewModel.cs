using StockManagement.Commands;
using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace StockManagement.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        public NavbarViewModel NavbarViewModel { get; }
        public NavigationStore NavigationStore { get; }




        private ObservableCollection<ProductsCategoriesList> _productsCategories;
        public ObservableCollection<ProductsCategoriesList> ProductsCategories { get { return _productsCategories; } }

        private ObservableCollection<ProductsCategoriesList> _filteredProductsCategories;
        public ObservableCollection<ProductsCategoriesList> FilteredProductsCategories
        {
            get { return _filteredProductsCategories; }
            set { _filteredProductsCategories = value; OnPropertyChanged(nameof(FilteredProductsCategories)); }
        }

        private ProductsCategoriesList _selectedProduct;
        public ProductsCategoriesList SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }

        //private string _searchKey;
        ////public string SearchKey
        ////{
        ////    get => _searchKey;
        ////    set { _searchKey = value; OnPropertyChanged(nameof(SearchKey)); RestFilteredProducts(); }
        ////}

        private int _totalInventoy;
        public int TotalInventoy
        {
            get => _totalInventoy;
            set { _totalInventoy = value; OnPropertyChanged(nameof(TotalInventoy)); }
        }

        private int _belowMinQuantity;
        public int BelowMinQuantity
        {
            get => _belowMinQuantity;
            set { _belowMinQuantity = value; OnPropertyChanged(nameof(BelowMinQuantity)); }
        }

        private int _closeToMaxQuantity;
        public int CloseToMaxQuantity
        {
            get => _closeToMaxQuantity;
            set { _closeToMaxQuantity = value; OnPropertyChanged(nameof(CloseToMaxQuantity)); }
        }

        private int _outOfStock;
        public int OutOfStock
        {
            get => _outOfStock;
            set { _outOfStock = value; OnPropertyChanged(nameof(OutOfStock)); }
        }

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

        private bool _isEditModeEnabled;
        public bool IsEditModeEnabled
        {
            get => _isEditModeEnabled;
            set
            {
                _isEditModeEnabled = value;
                OnPropertyChanged(nameof(IsEditModeEnabled));
            }
        }



        public ICommand DeleteProduct { get; }

        public ICommand ProductDetails { get; }

        public ICommand SearchCommand { get; }



        public HomeViewModel(NavigationStore navigationStore, NavbarViewModel navbarViewModel)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StockManagementDataSource");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(folder, "StockManagementDb.mdf")};Integrated Security=True";
            ProductMapper productsMapper = new ProductMapper(connectionString);
            this._isLoading = true;
            List<ProductsCategories> fetchedProducts = productsMapper.FindAllProductsCategories();
            _productsCategories = new ObservableCollection<ProductsCategoriesList>();
            foreach (ProductsCategories product in fetchedProducts)
            {
                ProductsCategories.Add(new ProductsCategoriesList(product, productsMapper));
            }
            CalStat(new List<ProductsCategoriesList>(_productsCategories));
            _filteredProductsCategories = _productsCategories;
            this._isLoading = false;
            NavigationStore = navigationStore;
            NavbarViewModel = navbarViewModel;
            DeleteProduct = new DeleteProductCommand(this, productsMapper);
            ProductDetails = new NavigationCommand<ProductDetailsViewModel>(NavigationStore, () => new ProductDetailsViewModel(this.SelectedProduct, NavbarViewModel));
            SearchCommand = new SearchCommand(this);
        }

        protected override void RestFilteredProducts()
        {
            FilteredProductsCategories = new ObservableCollection<ProductsCategoriesList>(ProductsCategories);
        }

        private void CalStat(List<ProductsCategoriesList> list)
        {
            int totalInventoy = 0;
            int outOfStock = 0;
            int closeToMaxQuantity = 0;
            int belowMinQuantity = 0;
            foreach (ProductsCategoriesList product in list)
            {
                if (product.Quantity > 0)
                {
                    totalInventoy += product.Quantity;

                }
                if (product.Quantity == 0)
                {
                    outOfStock++;
                }
                if (product.Quantity < product.MinQuantity && product.Quantity != 0)
                {
                    belowMinQuantity++;
                }
                if (product.MaxQuantity - product.Quantity == 2 || product.MaxQuantity - product.Quantity == 1)
                {
                    closeToMaxQuantity++;
                }
                _totalInventoy = totalInventoy;
                _outOfStock = outOfStock;
                _belowMinQuantity = belowMinQuantity;
                _closeToMaxQuantity = closeToMaxQuantity;
            }
        }
    }
}
