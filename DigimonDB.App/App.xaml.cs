using Microsoft.Extensions.DependencyInjection;

namespace DigimonDB.App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		AppDomain.CurrentDomain.UnhandledException += (_, e) =>
		{
			Console.Error.WriteLine($"[FATAL] UnhandledException: {e.ExceptionObject}");
			System.Diagnostics.Debug.WriteLine($"[FATAL] UnhandledException: {e.ExceptionObject}");
		};

		TaskScheduler.UnobservedTaskException += (_, e) =>
		{
			Console.Error.WriteLine($"[FATAL] UnobservedTaskException: {e.Exception}");
			System.Diagnostics.Debug.WriteLine($"[FATAL] UnobservedTaskException: {e.Exception}");
			e.SetObserved();
		};
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}