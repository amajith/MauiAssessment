using MauiAssessment.Views;

namespace MauiAssessment;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(AddProductView), typeof(AddProductView));
        Routing.RegisterRoute(nameof(ProductDetailView), typeof(ProductDetailView)); 
    }
}

