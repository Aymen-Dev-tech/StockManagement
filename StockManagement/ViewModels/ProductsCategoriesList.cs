using StockManagement.DALs;
using StockManagement.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace StockManagement.ViewModels
{
    public class ProductsCategoriesList : ProductsCategoriesListBase, IEditableObject
    {
        private ProductsCategories _productsCategories;
        private ProductMapper _productMapper;
        private ProductsCategories backupCopy;
        private bool inEdit;

        public ProductsCategoriesList(ProductsCategories productsCategories, ProductMapper productMapper) : base(productsCategories)
        {
            _productsCategories = productsCategories;
            _productMapper = productMapper;
        }

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = new ProductsCategories(this.ProductCode, this.ProductName, this.ProductImage, this.MaxQuantity, this.MinQuantity, this.Quantity,
                this.LastRestockTime, this.LastUsageTime, this.OrderQuantity, this.IsIncludedInOrder, this.Category);
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            try
            {
                if (this.Quantity > backupCopy.Quantity)
                {
                    this._productsCategories.LastRestockTime = DateTime.Now;
                }
                _productMapper.Update(this._productsCategories);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
            finally
            {
                inEdit = false;
                backupCopy = null;
            }

        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.ProductName = backupCopy.ProductName;
            this.Quantity = backupCopy.Quantity;
            this.MinQuantity = backupCopy.MinQuantity;
            this.MaxQuantity = backupCopy.MaxQauntity;
        }
    }

    public class ProductsCategoriesListValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ProductsCategoriesList productsCategoriesList = (value as BindingGroup).Items[0] as ProductsCategoriesList;
            if (productsCategoriesList.MinQuantity > productsCategoriesList.MaxQuantity)
            {
                return new ValidationResult(false, "Min Quantity must be less then the Max Quantity");
            }
            else if (productsCategoriesList.MaxQuantity < productsCategoriesList.MinQuantity)
            {
                return new ValidationResult(false, "Max Quantity must be grater then the Min Quantity");

            }
            else
            {
                return ValidationResult.ValidResult;
            }

        }
    }

}
