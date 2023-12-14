using AVA.Core.Settings;
using MUI;
using MUI.DI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AVA;

public static class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		//var proc = new Process();

		//proc.StartInfo.FileName = "notepad++";// FileName.ExpandEnvVars();
		////proc.StartInfo.Arguments = "C:/Windows/System32/drivers/etc/hosts"; // Arguments.ExpandEnvVars();
		//proc.StartInfo.WorkingDirectory = null; // WorkingDirectory.ExpandEnvVars();

		//proc.StartInfo.UseShellExecute = false;
		//proc.StartInfo.CreateNoWindow = true;

		//proc.StartInfo.RedirectStandardOutput = true;
		////proc.StartInfo.RedirectStandardInput = true;
		//proc.StartInfo.RedirectStandardError = true;

		//proc.Start();

		//Task.Run(async () =>
		//{
		//	while (!proc.StandardOutput.EndOfStream)
		//	{
		//		var line = await proc.StandardOutput.ReadLineAsync();

		//		Console.WriteLine($"OUTPUT: {line}");

		//		var dbg = 2;
		//	}
		//});

		//Task.Run(async () =>
		//{
		//	while (!proc.StandardError.EndOfStream)
		//	{
		//		var line = await proc.StandardError.ReadLineAsync();

		//		Console.WriteLine($"ERROR: {line}");

		//		var dbg = 2;
		//	}
		//});

		//proc.StartInfo.Verb = "runas";

		//proc.Dispose();



		//Console.ReadLine();
		//return;

		MUI.Utils.DependencyMapper.Map();

		var uiContext = new UIContext(600, 300);
		uiContext.RunOneFrame();

		var container = new Container()
			.Register<ResourceManager, ResourceManager>(c => uiContext.ResourceManager)
			.Register(uiContext)
			.Register<UI, UI>()
		;

		container.LoadPlugins(string.Empty.FromAppRoot());

		SettingsRoot.Instance.Initialize(container);

		RegisterServices(container);

		uiContext.PushUI(container.Resolve<UI>());

		using (uiContext)
		{
			uiContext.Run();
		}
	}

	private static void RegisterServices(IContainer container) =>
		AppDomain.CurrentDomain
		.GetAssemblies()
		.SelectMany(ass => ass.DefinedTypes)
		.Where(t => !t.IsAbstract)
		.Where(t => t.GetCustomAttribute<ServiceAttribute>(true) != null)
		.OrderBy(t => t.FullName)
		.ToList()
		.ForEach(s => container.Register(s.AsType(), s.GetCustomAttribute<ServiceAttribute>(true)!.Lifetime));
}