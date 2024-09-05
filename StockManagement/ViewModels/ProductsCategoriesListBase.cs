using StockManagement.DALs;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.ViewModels
{
    public class ProductsCategoriesListBase : ViewModelBase
    {
        private ProductsCategories _productsCategories;

      

        public int ProductCode
        {
            get => _productsCategories.ProductCode;
            set
            {
                _productsCategories.ProductCode = value;
                OnPropertyChanged(nameof(ProductCode));
            }
        }
        public string ProductName
        {
            get => _productsCategories.ProductName;
            set
            {
                _productsCategories.ProductName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string ProductImage
        {
            get => _productsCategories.ImagePath;
            set
            {
                _productsCategories.ImagePath = value;
                OnPropertyChanged(nameof(ProductImage));
            }
        }

        public int MinQuantity
        {
            get => _productsCategories.MinQuantity;
            set
            {
                _productsCategories.MinQuantity = value;
                OnPropertyChanged(nameof(MinQuantity));
            }
        }

        public int MaxQuantity
        {
            get => _productsCategories.MaxQauntity;
            set
            {
                _productsCategories.MaxQauntity = value;
                OnPropertyChanged(nameof(MaxQuantity));
            }
        }
        public int Quantity
        {
            get => _productsCategories.Quantity;
            set
            {
                _productsCategories.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DateTime? LastRestockTime

        {
            get => _productsCategories.LastRestockTime;
            set
            {
                _productsCategories.LastRestockTime = value;
                OnPropertyChanged(nameof(LastRestockTime));
            }
        }

        public DateTime? LastUsageTime

        {
            get => _productsCategories.LastUsageTime;
            set
            {
                _productsCategories.LastUsageTime = value;
                OnPropertyChanged(nameof(LastUsageTime));
            }
        }

        public int OrderQuantity
        {
            get => _productsCategories.OrderQuantity;
            set
            {
                _productsCategories.OrderQuantity = value;
                OnPropertyChanged(nameof(OrderQuantity));
            }
        }

        public bool IsIncludedInOrder
        {
            get => _productsCategories.IsIncludedInOrder;
            set
            {
                _productsCategories.IsIncludedInOrder = value;
                OnPropertyChanged(nameof(IsIncludedInOrder));
            }
        }
        public CategoryModel Category
        {
            get => _productsCategories.Category;
            set
            {
                _productsCategories.Category = value;
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(CategoryName));
                OnPropertyChanged(nameof(CategoryCode));

            }
        }

        public string CategoryName => Category.Name;
        public int CategoryCode => Category.Code;

        public ProductsCategoriesListBase(ProductsCategories productsCategories)
        {
            _productsCategories = productsCategories;
        }
    }
}
