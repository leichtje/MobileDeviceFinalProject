using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace MobileDeviceFinalProject;

public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<LocalDbService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<AddMedicationPage>();

#if ANDROID
    // Register the Android-specific notification service
    builder.Services.AddSingleton<INotificationService, NotificationService>();
#endif



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
