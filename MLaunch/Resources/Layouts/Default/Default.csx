#r "../../../MLaunch.Core.dll"
#r "../../../MLaunch.Plugins.dll"

using ImGuiNET;
using System;

var ctx = Get<MUI.UIContext>();
var sys = Get<MLaunch.Plugins.SysMon.SysMonService>();

ImGui.Text($"CPU {sys.CpuUsage.ToString("0.00")}");

ImGui.SameLine();
ImGui.Text($"Mem {sys.MemUsage.ToString("0.00")}");

foreach (var drive in sys.Drives)
{
    ImGui.SameLine();
    ImGui.Text($"{drive.Name} {drive.Usage.ToString("0.00")}");
}
