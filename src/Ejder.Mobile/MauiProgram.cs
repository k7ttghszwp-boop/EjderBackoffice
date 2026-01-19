using Microsoft.Extensions.Logging;

namespace Ejder.Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Services.AddSingleton(sp =>
		{
			var http = new HttpClient
			{
				// Android Emulator -> PC localhost erişimi:
				BaseAddress = new Uri("http://10.0.2.2:5185/")
			};
			return http;
		});

		builder.Services.AddSingleton<MainPage>();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
