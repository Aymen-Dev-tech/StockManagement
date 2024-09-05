using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
        public NavbarViewModel NavbarViewModel { get; }

        private readonly ProductsCategoriesList _product;
        public ProductsCategoriesList Product {  get { return _product; } }
       
        public ProductDetailsViewModel(ProductsCategoriesList product, NavbarViewModel navbarViewModel)
        {
            NavbarViewModel = navbarViewModel;
            _product = product;
        }
    }
}