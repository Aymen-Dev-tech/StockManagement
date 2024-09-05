using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.ViewModels
{
    public class CategoryList : ViewModelBase
    {
        private CategoryModel _category;

        public string Name => _category.Name;
        public int Code => _category.Code;

        public CategoryList(CategoryModel category)
        {
            _category = category;
        }
    }
}
