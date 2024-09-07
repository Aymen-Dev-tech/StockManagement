using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Commands
{
    public class OrdersSearchCommand : CommandBase
    {
        private readonly OrdersViewModel _ordersViewModel;

        public OrdersSearchCommand(OrdersViewModel ordersViewModel)
        {
            _ordersViewModel = ordersViewModel;
            _ordersViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

       

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_ordersViewModel.SearchKey) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_ordersViewModel.SearchKey))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {

            var searchKey = _ordersViewModel.SearchKey;
            _ordersViewModel.FilteredProductsCategories = new ObservableCollection<OrdersProductsCategories>
                (
                    _ordersViewModel.FilteredProductsCategories
                    .Where(product => product.ProductName.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0)
            );
        }
    }
}

