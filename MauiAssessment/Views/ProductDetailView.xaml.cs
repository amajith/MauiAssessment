using MauiAssessment.ViewModels;
namespace MauiAssessment.Views;

public partial class ProductDetailView : ContentPage
{
	public ProductDetailView(ProductDetailViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}
