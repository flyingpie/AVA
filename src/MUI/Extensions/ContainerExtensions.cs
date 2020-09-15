using MUI.DI;
using MUI.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MUI
{
	public static class ContainerExtensions
	{
		public static void LoadPlugins(this IContainer container, string pluginsDir)
		{
			var log = Log.Get(nameof(ContainerExtensions));

			log.Info($"Looking for plugins under directory '{pluginsDir}'...");

			if (Directory.Exists(pluginsDir))
			{
				var loadedDlls = AppDomain.CurrentDomain
					.GetAssemblies()
					.Select(ass => Uri.TryCreate(ass.CodeBase, UriKind.Absolute, out var path) ? path.LocalPath : null)
					.Where(path => path != null)
					.OrderBy(path => path)
					.ToList();

				var plugins = Directory
					.GetFiles(pluginsDir, "AVA.Plugin*.dll", SearchOption.AllDirectories)
					.Where(dll => !loadedDlls.Any(loadedDll => Path.GetFileName(loadedDll).Equals(Path.GetFileName(dll), StringComparison.OrdinalIgnoreCase)))
					.OrderBy(path => path)
					.ToList()
				;

				log.Info($"Found {plugins.Count} plugins");

				foreach (var plugin in plugins)
				{
					var pluginFileName = Path.GetFileName(plugin);
					log.Info($"Loading plugin '{pluginFileName}'...");

					try
					{
						var pluginAssName = AssemblyName.GetAssemblyName(plugin);
						var pluginAss = AppDomain.CurrentDomain.Load(pluginAssName);

						loadedDlls.Add(new Uri(pluginAss.CodeBase).LocalPath);

						log.Info($"Loaded plugin '{pluginFileName}'");
					}
					catch (Exception ex)
					{
						log.Error($"Error while loading plugin '{pluginFileName}': '{ex.Message}'");
					}
				}
			}
		}
	}
}