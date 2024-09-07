using StockManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace StockManagement.Commands
{
    public class PickImageCommand : CommandBase
    {
        private readonly AddProduct _addProduct;
        public PickImageCommand(AddProduct addProduct)
        {
            _addProduct = addProduct;
        }
        public override void Execute(object parameter)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png"
            };
            var dialogRes = openFileDialog.ShowDialog();
            if (dialogRes == true) // true indicates that a file was selected
            {
                _addProduct.ImageUrl =  openFileDialog.FileName;
            }

        }
    }
}
