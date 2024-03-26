using Microsoft.Extensions.Logging;
using MauiAssessment.Services;
using MauiAssessment.ViewModels;
using MauiAssessment.Views;

namespace MauiAssessment;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<ProductServices, SQLServices>();

        builder.Services.AddSingleton<ListProductsView>();
        builder.Services.AddTransient<AddProductView>();
        builder.Services.AddTransient<ProductDetailView>();

        builder.Services.AddSingleton<ListProductsViewModel>();
        builder.Services.AddTransient<AddProductViewmodel>();
        builder.Services.AddTransient<ProductDetailViewModel>();
        return builder.Build();
    }
}

