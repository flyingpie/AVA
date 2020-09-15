using System;
using System.Linq;
using System.Reflection;

namespace MUI.Utils
{
	public static class DependencyMapper
	{
		public static void Map()
		{
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
			{
				var assemblyName = new AssemblyName(args.Name);

				var assembly = AppDomain
					.CurrentDomain
					.GetAssemblies()
					.FirstOrDefault(ass => ass.GetName().Name.Equals(assemblyName.Name, StringComparison.OrdinalIgnoreCase))
				;

				return assembly;
			};
		}
	}
}