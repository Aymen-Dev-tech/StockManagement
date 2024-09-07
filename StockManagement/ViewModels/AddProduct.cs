using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Documents;
using System.Windows.Input;
using StockManagement.Commands;
using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.Stores;
using System.Windows.Media;
namespace StockManagement.ViewModels
{
    public class AddProduct : ViewModelBase
    {
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; OnPropertyChanged(nameof(CategoryName)); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        private int _maxQuantity;
        public int MaxQuantity
        {
            get { return _maxQuantity; }
            set { _maxQuantity = value; OnPropertyChanged(nameof(MaxQuantity)); }
        }

        private int _minQuantity;
        public int MinQuantity
        {
            get { return _minQuantity; }
            set { _minQuantity = value; OnPropertyChanged(nameof(MinQuantity)); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged(nameof(ImageUrl));}
        }

        private ObservableCollection<CategoryList> _categories;
        public ObservableCollection<CategoryList> Categories { get  { return _categories; } }

        private CategoryList _selectedCategory;
        public CategoryList SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private readonly CategoryMapper _categoryMapper;
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand NavigateToAddCategory {  get; }

        public ICommand PickImage { get; }
        
        public NavbarViewModel NavbarViewModel { get; }
        public AddProduct(NavigationStore navigationStore, NavbarViewModel navbarViewModel)
        {
            NavbarViewModel = navbarViewModel;
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StockManagementDataSource");
            string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Path.Combine(folder, "StockManagementDb.mdf")};Integrated Security=True";

            _categoryMapper = new CategoryMapper(connectionString);
            _categories = new ObservableCollection<CategoryList>();
            List<CategoryModel> fetchedCategories = _categoryMapper.findAll();
            foreach (CategoryModel category in fetchedCategories) { 
                _categories.Add(new CategoryList(category));
            }
            PickImage = new PickImageCommand(this);
            SubmitCommand = new AddProductCommand(this, new ProductMapper(connectionString));
            NavigateToAddCategory = new NavigationCommand<AddCategoryViewModel>(navigationStore, () => new AddCategoryViewModel(navigationStore, navbarViewModel));
        }
    }
}
