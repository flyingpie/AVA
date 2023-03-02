using MUI;
using MUI.Graphics;
using MUI.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AVA.Plugin.Macros
{
	public class Macro
	{
		public string Name { get; set; }

		public string Description
		{
			get => $"Macro - {Command}";
			set { }
		}

		public string DisplayName
		{
			get => Name;
			set => Name = value;
		}

		//public string IndexerName
		//{
		//	get => Command;
		//	set { }
		//}

		//public override int Boost
		//{
		//	get => 10;
		//	set { }
		//}

		public string Command { get; set; }

		public bool RunOnMatch { get; set; }

		public string Icon { get; set; }

		public string FileName { get; set; }

		public string Arguments { get; set; }

		public string WorkingDirectory { get; set; }

		public Dictionary<string, string> EnvironmentVars { get; set; }

		public bool RunAsAdmin { get; set; }

		public bool Execute()
		{
			var log = Log.Get(this);

			// TODO: Log
			try
			{
				var proc = new Process();

				proc.StartInfo.FileName = FileName.ExpandEnvVars();
				proc.StartInfo.Arguments = Arguments.ExpandEnvVars();
				proc.StartInfo.WorkingDirectory = WorkingDirectory.ExpandEnvVars();
				proc.StartInfo.UseShellExecute = true;

				if (EnvironmentVars != null)
				{
					foreach (var envVar in EnvironmentVars)
					{
						proc.StartInfo.Environment[envVar.Key] = envVar.Value.ExpandEnvVars();
					}
				}

				if (RunAsAdmin || Input.IsKeyDown(Keys.LeftControl))
				{
					proc.StartInfo.Verb = "runas";
				}

				proc.Start();
				proc.Dispose();

				return true;
			}
			catch (Exception ex)
			{
				log.Error($"Could not start process '{FileName}': {ex.Message}");
				return true;
			}
		}

		public Image GetIcon()
		{
			return ResourceManager.Instance.TryLoadImage(Icon.ExpandEnvVars().FromPluginRoot<Macro>(), out var icon)
				? icon
				: ResourceManager.Instance.DefaultImage;
		}
	}
}