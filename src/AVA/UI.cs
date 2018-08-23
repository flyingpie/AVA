using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Core.Settings;
using AVA.Plugins.SysMon;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.ImGuiControls;
using MUI.Logging;
using MUI.Win32.Input;
using System.Numerics;
using System.Threading.Tasks;

namespace AVA
{
    public class UI : UIBase
    {
        [Dependency] public AVASettings Settings { get; set; }

        [Dependency] private IQueryExecutorManager QueryExecutorManager { get; set; }

        [Dependency] private SysMonService SysMon { get; set; }

        private UIContext _uic;
        private QueryContext _queryContext;
        private ILog _log;

        private TextBox _queryBox;

        public UI()
        {
            _log = Log.Get(this);

            _uic = UIContext.Instance;
            _queryContext = new QueryContext();
            _queryBox = new TextBox();

            Width = 600;
            Height = 300;
        }

        public override void Load()
        {
            _uic.Opacity = .9f;

            Maximize();

            // Toggle on Alt-Space
            HotKeyManager.RegisterHotKey(Settings.ToggleUIKey, Settings.ToggleUIKeyModifiers);
            HotKeyManager.HotKeyPressed += (s, a) => Toggle();

            // Minimize on focus lost
            _uic.FocusGained += (s, a) => _log.Info("Gained focus");
            _uic.FocusLost += (s, a) => { _log.Info("Lost focus"); Minimize(); };
        }

        public override void Draw()
        {
            if (Input.IsKeyPressed(Keys.Escape)) Minimize();

            ImGui.PushStyleVar(StyleVar.WindowRounding, 0);

            ImGui.SetNextWindowPos(Vector2.Zero, Condition.Always);
            ImGui.SetNextWindowSize(new Vector2(_uic.Window.ClientBounds.Width, _uic.Window.ClientBounds.Height), Condition.Always);

            ImGui.BeginWindow(string.Empty, WindowFlags.NoMove | WindowFlags.NoResize | WindowFlags.NoTitleBar);
            {
                // Search bar
                ImGui.PushFont(Fonts.Regular24);

                DrawSearchBar();

                ImGui.Spacing();

                // Query executor
                ImGui.BeginChild("query-executor", new Vector2(ImGui.GetWindowContentRegionWidth(), ImGui.GetContentRegionAvailable().Y - 20), false, WindowFlags.Default);
                {
                    QueryExecutorManager.Draw();
                }
                ImGui.EndChild();

                // Footer
                DrawFooter();

                ImGui.PopFont();
            }
            ImGui.EndWindow();
        }

        public override async Task Update()
        {
            // Execute when ENTER was pressed
            if (Input.IsKeyPressed(Keys.Enter))
            {
                _log.Info($"ENTER");

                if (await QueryExecutorManager.TryExecuteAsync(_queryContext))
                {
                    if (_queryContext.HideUI) Minimize();

                    if (_queryContext.ResetText) _queryContext.Reset();
                }

                _queryBox.Focus();
            }
        }

        private void DrawSearchBar()
        {
            ImGui.PushFont(Fonts.Regular32);

            // Update input buffer based on the query context
            if (_queryBox.Text != _queryContext.Text)
            {
                _queryBox.Text = _queryContext.Text;
            }

            _queryBox.Draw();

            if (_queryContext.Focus)
            {
                _queryBox.Focus();

                _queryContext.Focus = false;
            }

            // Update the query context if the input buffer was changed
            if (_queryBox.IsChanged)
            {
                _queryContext.Text = _queryBox.Text;

                QueryExecutorManager.TryHandle(_queryContext);
            }

            _queryBox.ResetChanged();

            ImGui.PopFont();
        }

        private void DrawFooter()
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

        private void Toggle()
        {
            if (_uic.IsVisible) Minimize();
            else Maximize();
        }

        private void Maximize()
        {
            _log.Info("Maximize");

            _uic.CenterWindowToDisplayWithMouse();

            _uic.IsVisible = true;

            _uic.Focus();
            _queryBox.Focus();
        }

        private void Minimize()
        {
            Task.Run(() =>
            {
                _log.Info("Minimize");

                _queryContext.Reset();

                _uic.IsVisible = false;
            });
        }
    }
}