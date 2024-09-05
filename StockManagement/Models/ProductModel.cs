using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class ProductModel
    {
        public int Code;
        public string Name;
        public string Image;
        private int quantity;
        public int MinQuantity;
        public int MaxQuantity;
        public DateTime lastRestockDate;
        public DateTime lastUseDate;
        public int CategoryCode;
        public ProductModel(int code, string name, string image, int quantity, int minQuantity, int maxQuantity, int categoryCode)
        {
            Code = code;
            Name = name;
            Image = image;
            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;
            Quantity = quantity;
            CategoryCode = categoryCode;
        }

        public ProductModel()
        {
        }

        public int Quantity { get => quantity; set => quantity = value; }

        public void Create(ProductModel product)
        {
            //insert product to db
        }

        public ProductModel GetProduct(string productName)
        {
            return new ProductModel();
        }
        
        public void RemoveProduct(int Code)
        {
            //remove by code
        }


    }
}
