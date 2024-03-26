using MauiAssessment.Models;
using System;
using SQLite;

namespace MauiAssessment.Services
{
    public class SQLServices : ProductServices
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Product.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<Product>();
            }
        }

        public async Task<int> AddProduct(Product productModel)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(productModel);
        }

        public async Task<int> DeleteProduct(Product productModel)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(productModel);
        }

        public async Task<List<Product>> GetProductList()
        {
            await SetUpDb();
            var ProductList = await _dbConnection.Table<Product>().ToListAsync();
            return ProductList;
        }

        public async Task<int> UpdateProduct(Product productModel)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(productModel);
        }
    }
}

