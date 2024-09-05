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
    public class SearchCommand : CommandBase
    {
        private readonly HomeViewModel _homeViewModel;

        public SearchCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public override void Execute(object parameter)
        {   

            var searchKey = _homeViewModel.SearchKey;
            _homeViewModel.FilteredProductsCategories = new ObservableCollection<ProductsCategoriesList>
                (
                    _homeViewModel.FilteredProductsCategories
                    .Where(product => product.ProductName.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0)
            );
            foreach (ProductsCategoriesList productsCategoriesList in _homeViewModel.FilteredProductsCategories)
            {
                Console.WriteLine(productsCategoriesList.ProductName);
            }

        }
    }
}
