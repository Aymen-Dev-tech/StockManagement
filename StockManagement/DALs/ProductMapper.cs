using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace StockManagement.DALs
{
    public class ProductMapper
    {
        private string _connectionString;
        private IDbConnection _databaseConnection;

        public ProductMapper(string connectionString)
        {
            _connectionString = connectionString;

        }

        private bool ConnectToDatabase(string connectionString)
        {
            bool success = false;
            _databaseConnection = new SqlConnection(connectionString);
            try
            {
                _databaseConnection.Open();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        private void DisconnectFromDatabase()
        {
            if (_databaseConnection.State != ConnectionState.Open)
            {
                _databaseConnection.Close();
            }
        }

        private IDataReader ExecuteReadCommand(IDbCommand command)
        {
            IDataReader dataReader = null;
            if (_databaseConnection.State == ConnectionState.Open)
            {
                dataReader = command.ExecuteReader();
            }
            return dataReader;
        }
        private ProductModel CreateProductFromReader(IDataReader dataReader)
        {
            int code = dataReader.GetInt32(0);
            string name = dataReader.GetString(1);
            string image = null;
            if(!dataReader.IsDBNull(2)) image = dataReader.GetString(2);
            int minQuantity = dataReader.GetInt32(3);
            int maxQuantity = dataReader.GetInt32(4);
            int quantity = dataReader.GetInt32(4);
            int categoryCode = dataReader.GetInt32(7);

            return new ProductModel(code, name, image, minQuantity, maxQuantity, quantity, categoryCode);

        }

        private ProductsCategories CreateProductsCategoriesFromReader(IDataReader dataReader)
        {
            int productCode = dataReader.GetInt32(0);
            string productName = dataReader.GetString(1);
            string productImage = null;
            if(!dataReader.IsDBNull(2)) productImage = dataReader.GetString(2);
            int productMinQuantity = dataReader.GetInt32(3);
            int productMaxQuantity = dataReader.GetInt32(4);
            int quantity = dataReader.GetInt32(5);
            DateTime? lastRestockDate = null;
            if (!dataReader.IsDBNull(6)) lastRestockDate = dataReader.GetDateTime(6);
            DateTime? lastUsageDate = null;
            if (!dataReader.IsDBNull(7)) lastUsageDate = dataReader.GetDateTime(7);
            int categoryCode = dataReader.GetInt32(8);
            string categoryName = dataReader.GetString(9);
            return new ProductsCategories(
                productCode, productName,
                productImage, productMaxQuantity,
                productMinQuantity, quantity, lastRestockDate, lastUsageDate, 0, false,
                new CategoryModel(categoryCode, categoryName));

        }

        public void Insert(ProductModel productModel)
        {
            ConnectToDatabase(_connectionString);

            string insertUserCommandText = string.Format(
                "INSERT INTO Products (Code, Name, Image, MinQuantity, MaxQuantity, Quantity, CategoryCode) " +
                "OUTPUT INSERTED.Code " +
                "VALUES (@Code, @Name, @Image, @MinQuantity, @MaxQuantity, @Quantity, @CategoryCode)");
            using (IDbCommand insertCommand = new SqlCommand(insertUserCommandText, (SqlConnection)_databaseConnection))
            {
                // Use parameters to prevent SQL injection and ensure safe data handling
                insertCommand.Parameters.Add(new SqlParameter("@Code", productModel.Code));
                insertCommand.Parameters.Add(new SqlParameter("@Name", productModel.Name));
                if (String.IsNullOrEmpty(productModel.Image))
                    insertCommand.Parameters.Add(new SqlParameter("@Image", DBNull.Value));
                else
                    insertCommand.Parameters.Add(new SqlParameter("@Image", productModel.Image));

                insertCommand.Parameters.Add(new SqlParameter("@MinQuantity", productModel.MinQuantity));
                insertCommand.Parameters.Add(new SqlParameter("@MaxQuantity", productModel.MaxQuantity));
                insertCommand.Parameters.Add(new SqlParameter("@Quantity", productModel.Quantity));
                insertCommand.Parameters.Add(new SqlParameter("@CategoryCode", productModel.CategoryCode));

                // Execute the command and retrieve the newly inserted Code value
                insertCommand.ExecuteScalar();

            }

        }

        private List<ProductsCategories> Find(string sqlCommand)
        {
            ConnectToDatabase(_connectionString);
            IDbCommand findByIDCommandCommand = new SqlCommand(sqlCommand, (SqlConnection)_databaseConnection);
            IDataReader productsReader = ExecuteReadCommand(findByIDCommandCommand);
            List<ProductsCategories> productsCategories = new List<ProductsCategories>();
            while (productsReader.Read())
            {
                productsCategories.Add(CreateProductsCategoriesFromReader(productsReader));
            }
            DisconnectFromDatabase();
            return productsCategories;
        }

        public List<ProductsCategories> FindAllProductsCategories()
        {
            string sqlCommand = @"
                        SELECT 
                            Products.Code AS ProductCode, 
                            Products.Name AS ProductName, 
                            Products.Image AS ProductImage,
                            Products.MinQuantity AS MinQuantity,
                            Products.MaxQuantity AS MaxQuantity,
                            Products.Quantity,
                            Products.LastRestockDate,
                            Products.LastUsageTime,
                            Categories.Code AS CategoryCode,
                            Categories.Name AS CategoryName 
                        FROM 
                            Products 
                        INNER JOIN 
                            Categories 
                        ON 
                            Products.CategoryCode = Categories.Code";
            return Find(sqlCommand);
        }



        public void Update(ProductsCategories productsCategories)
        {
            ConnectToDatabase(_connectionString);

            string updateProductCategoryCommand = "UPDATE Products SET Name = @Name, Quantity = @Quantity," +
                "MinQuantity = @MinQuantity, MaxQuantity = @MaxQuantity," +
                " LastRestockDate = @LastRestockDate, LastUsageTime = @LastUsageTime" +
                " WHERE Code = @Code";
            Console.WriteLine($"{productsCategories.MinQuantity}, {productsCategories.MaxQauntity}");
            using (IDbCommand updateCommand = new SqlCommand(updateProductCategoryCommand, (SqlConnection)_databaseConnection))
            {
                updateCommand.Parameters.Add(new SqlParameter("@Name", productsCategories.ProductName));
                updateCommand.Parameters.Add(new SqlParameter("@Quantity", productsCategories.Quantity));
                updateCommand.Parameters.Add(new SqlParameter("@MinQuantity", productsCategories.MinQuantity));
                updateCommand.Parameters.Add(new SqlParameter("@MaxQuantity", productsCategories.MaxQauntity));

                if (productsCategories.LastRestockTime.HasValue)
                    updateCommand.Parameters.Add(new SqlParameter("@LastRestockDate", productsCategories.LastRestockTime.Value));
                else
                    updateCommand.Parameters.Add(new SqlParameter("@LastRestockDate", DBNull.Value));

                if (productsCategories.LastUsageTime.HasValue)
                    updateCommand.Parameters.Add(new SqlParameter("@LastUsageTime", productsCategories.LastUsageTime.Value));
                else
                    updateCommand.Parameters.Add(new SqlParameter("@LastUsageTime", DBNull.Value));
                updateCommand.Parameters.Add(new SqlParameter("@Code", productsCategories.ProductCode));

                updateCommand.ExecuteNonQuery();
            }

            DisconnectFromDatabase();

        }

        public void Delete(int productCode)
        {
            ConnectToDatabase(_connectionString);

            string deleteProductCategoryCommand = "DELETE FROM Products WHERE Code = @Code";

            using (IDbCommand deleteCommand = new SqlCommand(deleteProductCategoryCommand, (SqlConnection)_databaseConnection))
            {
                deleteCommand.Parameters.Add(new SqlParameter("@Code", productCode));
                deleteCommand.ExecuteNonQuery();
            }

            DisconnectFromDatabase();
        }
    }



}
