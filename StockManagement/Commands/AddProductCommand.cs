using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace StockManagement.Commands
{
    public class AddProductCommand : CommandBase
    {
        private readonly AddProduct _addProduct;
        private readonly ProductMapper productMapper;

        public AddProductCommand(AddProduct addProduct, ProductMapper productMapper)
        {
            _addProduct = addProduct;
            _addProduct.PropertyChanged += OnViewModelPropertyChanged;
            this.productMapper = productMapper;
        }


        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_addProduct.ProductName) &&
                _addProduct.SelectedCategory != null &&
                base.CanExecute(parameter);
        }


        public override void Execute(object parameter)
        {
            Guid guid = Guid.NewGuid();

            int hashCode = guid.GetHashCode();
            hashCode = Math.Abs(hashCode);
            ProductModel product = new ProductModel(hashCode, _addProduct.ProductName, _addProduct.ImageUrl, _addProduct.Quantity, _addProduct.MinQuantity
                , _addProduct.MaxQuantity, _addProduct.SelectedCategory.Code);
            try
            {
                productMapper.Insert(product);
                MessageBox.Show("Product Created Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Type type = _addProduct.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite && (property.PropertyType.IsClass ||
                        Nullable.GetUnderlyingType(property.PropertyType) != null))
                    {
                        property.SetValue(_addProduct, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddProduct.ProductName) || e.PropertyName == nameof(AddProduct.SelectedCategory))
            {
                OnCanExecuteChanged();
            }
        }

    }
}
