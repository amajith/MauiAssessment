using MauiAssessment.ViewModels;
namespace MauiAssessment.Views;

public partial class AddProductView : ContentPage
{
    private AddProductViewmodel _viewMode;
    private string date;
    public AddProductView(AddProductViewmodel viewModel)
    {
        InitializeComponent();
        datePickerValue.MaximumDate = DateTime.Today;
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //_viewMode.CategoryViewModel();
        SetImage();

    }

    void DatePicker_DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
        date = datePickerValue.Date.ToString();
        _viewMode.DateValue = date.Split()[0];
    }

    private void SetImage()
    {
        if (_viewMode.ProductDetail.ProductPhoto is not null)
        {
            Binding productBinding = new Binding
            {
                Source = _viewMode.ProductDetail,
                Path = _viewMode.ProductDetail.ProductPhoto
            };
            product_image.Source = _viewMode.ProductDetail.ProductPhoto;
            _viewMode.CompleteProductPhotoPath = _viewMode.ProductDetail.ProductPhoto;
            product_image.IsVisible = true;
        }
    }
}
