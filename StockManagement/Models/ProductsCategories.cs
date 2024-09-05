using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Models
{
    public class ProductsCategories
    {
        public int ProductCode { get; set; }
        public string ProductName { get; set; }

        public string ImagePath { get; set; }

        public int MaxQauntity { get; set; }

        public int MinQuantity { get; set; }

        public int Quantity { get; set; }

        public DateTime? LastRestockTime { get; set; }

        public DateTime? LastUsageTime { get; set; }

        public int OrderQuantity { get; set; }

        public bool IsIncludedInOrder { get; set; }

        public CategoryModel Category { get; set; }



        public ProductsCategories(int productCode, string productName, string imagePath, int maxQauntity, int minQuantity, int quantity,
    DateTime? lastRestockTime, DateTime? lastUsageTime, int orderQuantity, bool isIncludedInOrder, CategoryModel category)
        {
            ProductCode = productCode;
            ProductName = productName;
            ImagePath = imagePath;
            MaxQauntity = maxQauntity;
            MinQuantity = minQuantity;
            Quantity = quantity;
            LastRestockTime = lastRestockTime;
            LastUsageTime = lastUsageTime;
            OrderQuantity = orderQuantity;
            Category = category;
            IsIncludedInOrder = isIncludedInOrder;
        }

        public override string ToString()
        {
            return "Product: " + ProductName + " " + LastRestockTime + " " + Quantity + " " + IsIncludedInOrder + " " + MaxQauntity + " " + MinQuantity;
        }



    }
}
