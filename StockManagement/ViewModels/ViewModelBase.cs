using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private string _searchKey;
        public string SearchKey
        {
            get => _searchKey;
            set
            {
                _searchKey = value; OnPropertyChanged(nameof(SearchKey));
                if (string.IsNullOrEmpty(_searchKey)) RestFilteredProducts();

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RestFilteredProducts()
        {

        }
    }
}
