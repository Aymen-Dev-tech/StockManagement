using StockManagement.DALs;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.ViewModels
{
    public class OrdersProductsCategories : ProductsCategoriesListBase
    {
        private ProductsCategories _productsCategories;
        public OrdersProductsCategories(ProductsCategories productsCategories) : base(productsCategories)
        {
            _productsCategories = productsCategories;
        }
    }
}
