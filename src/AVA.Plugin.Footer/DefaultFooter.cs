using AVA.Core.Footers;
using AVA.Core.Providers;
using ImGuiNET;
using MUI;
using MUI.DI;
using System.Linq;
using System.Numerics;

namespace AVA.Plugin.Footer
{
	[Service]
	public class SysMonFooter : IFooter
	{
		[Dependency] private SysMonService SysMon { get; set; }

		[Dependency] private INowPlayingProvider[] NowPlayingProviders { get; set; }

		public int Priority => 999;

		public void Draw()
		{
			ImGui.PushFont(Fonts.Regular16);
			ImGui.BeginChild("footer", ImGui.GetContentRegionAvail(), ImGuiChildFlags.None, ImGuiWindowFlags.None);
			{
				// SysMon
				ImGui.Text($"CPU {SysMon.CpuUsage.ToString("0.00")}");

				ImGui.SameLine();
				ImGui.Text($"Mem {SysMon.MemUsage.ToString("0.00")}");

				foreach (var drive in SysMon.Drives)
				{
					ImGui.SameLine();
					ImGui.Text($"{drive.Name} {drive.Usage.ToString("0.00")}");
				}

				string nowPlaying = null;
				var nowPlayingProvider = NowPlayingProviders.FirstOrDefault(np => np.TryGetNowPlaying(out nowPlaying));
				if (nowPlayingProvider != null)
				{
					ImGui.SameLine();

					var curPos = ImGui.GetCursorScreenPos();
					var avail = ImGui.GetContentRegionAvail();
					var textSize = ImGui.CalcTextSize(nowPlaying);

					ImGui.SetCursorScreenPos(new Vector2(curPos.X + avail.X - textSize.X, curPos.Y));
					ImGui.Text(nowPlaying);
				}
			}
			ImGui.EndChild();
			ImGui.PopFont();
		}
	}
}