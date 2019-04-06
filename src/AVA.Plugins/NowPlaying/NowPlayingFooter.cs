//using AVA.Core.Footers;
//using ImGuiNET;
//using MUI;
//using MUI.DI;

//namespace AVA.Plugins.NowPlaying
//{
//    [Service]
//    public class NowPlayingFooter : IFooter
//    {
//        public int Priority => 99;

//        public void Draw()
//        {
//            ImGui.PushFont(Fonts.Regular16);
//            ImGui.BeginChild("footer_now-playing", false, WindowFlags.Default);
//            {
//                // SysMon
//                //ImGui.Text($"CPU {SysMon.CpuUsage.ToString("0.00")}");

//                //ImGui.SameLine();
//                //ImGui.Text($"Mem {SysMon.MemUsage.ToString("0.00")}");

//                //foreach (var drive in SysMon.Drives)
//                //{
//                //    ImGui.SameLine();
//                //    ImGui.Text($"{drive.Name} {drive.Usage.ToString("0.00")}");
//                //}

//                ImGui.Text("Sup");
//            }
//            ImGui.EndChild();
//            ImGui.PopFont();
//        }
//    }
//}