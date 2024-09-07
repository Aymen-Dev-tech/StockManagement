using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class CategoryModel
    {
        public int Code;
        public string Name;
        public List<ProductModel> Products;
        public CategoryModel(int code, string name)
        {
            Code = code;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public CategoryModel(string name)
        {
           Name = name;
        }
    }
}
