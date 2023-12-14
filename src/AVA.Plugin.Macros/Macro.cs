using MUI;
using MUI.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

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
			using var process = new ProcessRunner(
				fileName: FileName,
				arguments: Arguments,
				workingDirectory: WorkingDirectory,
				environmentVariables: EnvironmentVars?.Select(e => (e.Key, e.Value)),
				runAsAdmin: RunAsAdmin || Input.IsKeyDown(Keys.LeftControl));

			process.Start();

			return true;
		}

		public Image GetIcon()
		{
			return ResourceManager.Instance.TryLoadImage(Icon.ExpandEnvVars().FromPluginRoot<Macro>(), out var icon)
				? icon
				: ResourceManager.Instance.DefaultImage;
		}
	}
}