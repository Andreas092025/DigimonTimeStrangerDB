using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Sinks.File;
using DigimonDB.Core.Data;
using DigimonDB.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace DigimonDB.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		var logDir = Path.Combine(FileSystem.Current.AppDataDirectory, "Logs");
		Directory.CreateDirectory(logDir);

		var logFile = Path.Combine(logDir, "app-.log");

		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Debug()
			.WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
			.CreateLogger();

		builder.Logging.ClearProviders();
		builder.Logging.AddSerilog(Log.Logger, dispose: true);

		AppDomain.CurrentDomain.UnhandledException += (_, e) =>
		{
			Log.Fatal(e.ExceptionObject as Exception, "Unhandled exception");
		};

		TaskScheduler.UnobservedTaskException += (_, e) =>
		{
			Log.Fatal(e.Exception, "Unobserved task exception");
			e.SetObserved();
		};

	
		var dbPath = Path.Combine(FileSystem.Current.AppDataDirectory, "digimon.db");
		SeedDatabaseIfAvailable(dbPath);

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddDbContext<DigimonContext>(options =>
			options.UseSqlite($"Data Source={dbPath}"));

		builder.Services.AddScoped<DigimonService>();
		builder.Services.AddScoped<ItemService>();
		builder.Services.AddScoped<DashboardService>();
		builder.Services.AddScoped<EvolutionService>();
		builder.Services.AddScoped<ImportService>();
		builder.Services.AddTransient<Pages.DatabasePage>();
		builder.Services.AddTransient<Pages.DigimonPage>();
		builder.Services.AddTransient<Pages.ItemsPage>();
		builder.Services.AddTransient<Pages.DigivolutionPlannerPage>();
		builder.Services.AddTransient<Pages.AboutPage>();

#if DEBUG
		builder.Logging.AddDebug();
		builder.Logging.AddConsole();
#endif

		var app = builder.Build();
		using var scope = app.Services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<DigimonContext>();
		context.Database.EnsureCreated();

		return app;
	}

	private static void SeedDatabaseIfAvailable(string targetDbPath)
	{
		if (File.Exists(targetDbPath))
		{
			return;
		}

		var candidatePaths = new[]
		{
			Path.Combine(AppContext.BaseDirectory, "SeedData", "digimon.db"),
			Path.Combine(AppContext.BaseDirectory, "digimon.db")
		};

		var sourcePath = candidatePaths.FirstOrDefault(File.Exists);
		if (sourcePath is null)
		{
			return;
		}

		Directory.CreateDirectory(Path.GetDirectoryName(targetDbPath)!);
		File.Copy(sourcePath, targetDbPath, overwrite: false);
	}
}
