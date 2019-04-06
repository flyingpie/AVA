using AVA.Core.Footers;
using ImGuiNET;
using MUI;
using MUI.DI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace AVA.Plugins.SysMon
{
    [Service]
    public class SysMonFooter : IFooter
    {
        [Dependency] private SysMonService SysMon { get; set; }

        public int Priority => 999;

        public void Draw()
        {
            ImGui.PushFont(Fonts.Regular16);
            ImGui.BeginChild("footer", false, WindowFlags.Default);
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

                ImGui.SameLine();
                var trackInfo = GetSpotifyTrackInfo();
                var req = ImGui.GetTextSize(trackInfo);
                var av = ImGui.GetContentRegionAvailableWidth();

                var pos = ImGui.GetCursorScreenPos();
                ImGui.SetCursorScreenPos(pos + new Vector2(av - req.X, 0));
                ImGui.Text(trackInfo);
            }
            ImGui.EndChild();
            ImGui.PopFont();
        }

        private DateTime _lastPoll = DateTime.MinValue;
        private TimeSpan _interval = TimeSpan.FromSeconds(2);
        private string _lastTrack;

        private string GetSpotifyTrackInfo()
        {
            if (_lastPoll <= DateTime.Now.Subtract(_interval))
            {
                _lastPoll = DateTime.Now;

                var proc = Process.GetProcessesByName("Spotify").FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

                if (proc == null)
                {
                    _lastTrack = "";
                    return _lastTrack;
                }

                if (string.Equals(proc.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase))
                {
                    _lastTrack = "Paused";
                    return _lastTrack;
                }

                _lastTrack = proc.MainWindowTitle;
            }

            return _lastTrack;
        }
    }
}