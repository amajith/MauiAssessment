using System;
using MauiAssessment.Models;

namespace MauiAssessment.Services
{
    public interface ProductServices
    {
        Task<List<Product>> GetProductList();
        Task<int> AddProduct(Product productModel);
        Task<int> DeleteProduct(Product productModel);
        Task<int> UpdateProduct(Product productModel);
    }
}

