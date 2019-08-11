using ImGuiNET;
using MUI;
using System.Numerics;
using System.Threading.Tasks;

namespace AVA.Core
{
    public class SettingsUI : UIBase
    {
        public SettingsUI()
        {
            Width = 900;
            Height = 700;
        }

        public override Task Update()
        {
            return base.Update();
        }

        public override void Draw()
        {
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);

            ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
            ImGui.SetNextWindowSize(new Vector2(UIContext.Instance.Window.ClientBounds.Width, UIContext.Instance.Window.ClientBounds.Height), ImGuiCond.Always);

            ImGui.Begin(string.Empty, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar);
            {
                ImGui.Text("Surprise motherfucker!");
                if (ImGui.Button("Close"))
                {
                    UIContext.Instance.PopUI();
                }
            }
            ImGui.End();
        }
    }
}