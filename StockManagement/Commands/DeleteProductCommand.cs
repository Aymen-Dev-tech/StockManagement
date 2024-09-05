using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockManagement.Commands
{
    public class DeleteProductCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        //private readonly ProductsCategoriesList _selectedProduct;
        private readonly ProductMapper _productMapper;
        //private readonly ObservableCollection<ProductsCategoriesList> _productsCategories;

        public DeleteProductCommand(HomeViewModel homeViewModel, ProductMapper productMapper)
        {
            _homeViewModel = homeViewModel;
            _productMapper = productMapper;
        }

        public override void Execute(object parameter)
        {
            if (_homeViewModel.SelectedProduct.Quantity > 0)
            {
                MessageBox.Show("Product still in stock", "Can't Delete Product", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
               "Are you sure you want to delete this product?",
               "Confirm Delete",
               MessageBoxButton.YesNo,
               MessageBoxImage.Warning);
            if (result == MessageBoxResult.No) return;
            try
            {
                //delete product from the DB
                _productMapper.Delete(_homeViewModel.SelectedProduct.ProductCode);
                //delete product from the observableCollection (update the UI)
                var productToDelete = _homeViewModel.ProductsCategories.FirstOrDefault(p => p.ProductCode == _homeViewModel.SelectedProduct.ProductCode);
                if (productToDelete != null)
                {
                    _homeViewModel.ProductsCategories.Remove(productToDelete);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}
