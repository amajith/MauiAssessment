using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAssessment.Models;
using System;
namespace MauiAssessment.ViewModels
{
    [QueryProperty(nameof(ProductDetail), "ProductDetail")]
    public partial class ProductDetailViewModel: ObservableObject
    {
        [ObservableProperty]
        private Product _productDetail = new Product();
	}
}

