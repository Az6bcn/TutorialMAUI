using TutorialMAUI.Services;
using TutorialMAUI.View;
using TutorialMAUI.ViewModel;

namespace TutorialMAUI;

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

        builder.Services.AddSingleton<MonkeyService>();
        builder.Services.AddSingleton<MonkeyViewModel>();
        builder.Services.AddSingleton<MonkeyDetailsViewModel>();

        // register the page so that the shell can get it along with all of its dependencies i.e the page will use the viewmodel, which depends on the monkey service
        builder.Services.AddSingleton<MainPage>();

        // Transient because we'll create a new details page for different monkeys everytime
        builder.Services.AddTransient<DetailsPage>();


        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

        return builder.Build();
	}
}
