using Microsoft.Data.SqlClient;
using StockManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StockManagement.DALs
{
    public class CategoryMapper
    {
        private string _connectionString;
        private IDbConnection _dbConnection;

        public CategoryMapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        private bool ConnectToDatabase(string connectionString)
        {
            bool success = false;
            _dbConnection = new SqlConnection(connectionString);
            try
            {
                _dbConnection.Open();
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
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }

        private IDataReader ExecuteReadCommand(IDbCommand command)
        {
            IDataReader dataReader = null;
            if (_dbConnection.State == ConnectionState.Open)
            {
                dataReader = command.ExecuteReader();
            }
            return dataReader;
        }

        private CategoryModel CreateCategoryFromReader(IDataReader reader) {
            int code = reader.GetInt32(0);
            string name = reader.GetString(1);
            return new CategoryModel(code, name);
        }

        private List<CategoryModel> Find(string sqlCommand)
        {
            ConnectToDatabase(_connectionString);
            IDbCommand findByIDCommandCommand = new SqlCommand(sqlCommand, (SqlConnection)_dbConnection);
            IDataReader categoryReader = ExecuteReadCommand(findByIDCommandCommand);
            List<CategoryModel> categories = new List<CategoryModel>();
            while (categoryReader.Read())
            {
                categories.Add(CreateCategoryFromReader(categoryReader));
            }
            DisconnectFromDatabase();
            return categories;
        }

        public List<CategoryModel> findAll()
        {
            return Find("SELECT * FROM Categories");
        }

        public void InsertCategory(CategoryModel category)
        {
            ConnectToDatabase(_connectionString);
            string insertCategoryCommandText = string.Format("INSERT INTO Categories (Code, Name) VALUES (@Code, @Name)");
            using (IDbCommand insertCategoryCommand = new SqlCommand(insertCategoryCommandText, (SqlConnection)_dbConnection))
            {
                insertCategoryCommand.Parameters.Add(new SqlParameter("@Code", category.Code));
                insertCategoryCommand.Parameters.Add(new SqlParameter("@Name", category.Name));
                insertCategoryCommand.ExecuteScalar();
            }
        }

    }
}
