using StockManagement.DALs;
using StockManagement.Models;
using StockManagement.ViewModels;
using System;
using System.Windows;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace StockManagement.Commands
{
    public class AddCategoryCommand : CommandBase
    {

        private readonly AddCategoryViewModel _addCategory;
        private readonly CategoryMapper _categoryMapper;
        public AddCategoryCommand(AddCategoryViewModel addCategory, CategoryMapper categoryMapper)
        {
            _addCategory = addCategory;
            _addCategory.PropertyChanged += OnViewModelPropertyChanged;
            _categoryMapper = categoryMapper;

        }
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_addCategory.CategoryName) && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Guid guid = Guid.NewGuid();
            int hashCode = guid.GetHashCode();
            hashCode = Math.Abs(hashCode);
            CategoryModel category = new CategoryModel(hashCode, _addCategory.CategoryName);
            try
            {
                _categoryMapper.InsertCategory(category);
                MessageBox.Show("Category Created Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show($"Category with the name {_addCategory.CategoryName} already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddCategoryViewModel.CategoryName))
            {
                OnCanExecuteChanged();
            }
        }

    }
}
