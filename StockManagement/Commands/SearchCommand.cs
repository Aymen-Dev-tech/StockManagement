using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            _homeViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }



        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_homeViewModel.SearchKey) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_homeViewModel.SearchKey))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {

            var searchKey = _homeViewModel.SearchKey;
            _homeViewModel.FilteredProductsCategories = new ObservableCollection<ProductsCategoriesList>
                (
                    _homeViewModel.FilteredProductsCategories
                    .Where(product => product.ProductName.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0)
            );


        }
    }
}
