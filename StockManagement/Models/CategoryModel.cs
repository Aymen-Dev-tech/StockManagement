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

        public void Create(int Code, string Name)
        {
           //insert to db
        }

        public List<ProductModel> GetAllProducts()
        {
            return new List<ProductModel>();
        }

        public List<CategoryModel> GetAllCateories()
        {
            return new List<CategoryModel>();

        }

        public void addProduct(int code)
        {
            //insert product to category
        }

        public void removeProduct()
        {

        }

        public void moveProduct()
        {

        }

    }
}
