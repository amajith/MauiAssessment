using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAssessment.Models;
using MauiAssessment.Services;
using MauiAssessment.Views;
using System;
using System.Collections.ObjectModel;

namespace MauiAssessment.ViewModels
{
	public partial class ListProductsViewModel : ObservableObject
	{
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        private readonly ProductServices _productService;

        [ObservableProperty]
        public bool isCollectionViewVisible = false;

        public ListProductsViewModel(ProductServices productService)
        {
            _productService = productService;
        }



        [RelayCommand]
        public async void GetProductList()
        {
            Products.Clear();
            var products = await _productService.GetProductList();
            if (products?.Count > 0)
            {
                IsCollectionViewVisible = true;
                products = products.OrderBy(f => f.Name).ToList();
                foreach (var product in products)
                {
                    Products.Add(product);

                }
            }
            else {
                IsCollectionViewVisible = false;
                return;
            }
        }

        [RelayCommand]
        async Task AddUpdateProduct()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(AddProductView), true);
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert($"Error: {e}", "Something went wrong please restart the app", "OK");
            }
        }

        [RelayCommand]
        public async void EditProduct(Product product)
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ProductDetail", product);
            await AppShell.Current.GoToAsync(nameof(AddProductView), true, navParam);
        }

        [RelayCommand]
        public async void DeleteProduct(Product product)
        {
            bool answer = await Shell.Current.DisplayAlert("Alert!", "Do you want to Delete the current Product?", "Yes", "No");
            if (answer)
            {
                var delResponse = await _productService.DeleteProduct(product);
                if (delResponse > 0)
                {
                    GetProductList();
                }
            }
            
        }

        [RelayCommand]
        public async void ProductDetail(Product product)
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("ProductDetail", product);
            await AppShell.Current.GoToAsync(nameof(ProductDetailView), true, navParam);
        }
           
    }
}

