﻿using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using SQLitePCL;

namespace MobileDev;

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
#if !WINDOWS
		builder.UseLocalNotification();
#endif

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<DatabaseHandler>(s => ActivatorUtilities.CreateInstance<DatabaseHandler>(s));
		return builder.Build();
	}
}
