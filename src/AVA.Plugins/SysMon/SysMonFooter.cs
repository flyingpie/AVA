using AVA.Core.Footers;
using ImGuiNET;
using MUI;
using MUI.DI;

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
            }
            ImGui.EndChild();
            ImGui.PopFont();
        }
    }
}