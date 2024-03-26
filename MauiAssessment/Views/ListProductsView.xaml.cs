using MauiAssessment.ViewModels;
namespace MauiAssessment.Views;

public partial class ListProductsView : ContentPage
{
    private ListProductsViewModel _viewMode;
    public ListProductsView(ListProductsViewModel viewModel)
    {
        InitializeComponent();
        _viewMode = viewModel;

        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewMode.GetProductListCommand.Execute(null);
    }
}
