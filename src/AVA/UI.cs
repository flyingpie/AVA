using AVA.Core;
using AVA.Core.QueryExecutors;
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
        [Dependency] public UIContext Context { get; set; }

        [Dependency] private IQueryExecutorManager QueryExecutorManager { get; set; }

        [Dependency] private SysMonService SysMon { get; set; }

        private QueryContext _queryContext;

        private TextBox _queryBox;

        private ILog _log;

        public UI()
        {
            _log = Log.Get(this);
            _queryContext = new QueryContext();
            _queryBox = new TextBox();
        }

        public override void Load()
        {
            Context.Opacity = .9f;

            Maximize();

            // TODO: Move to settings
            // Toggle on Alt-Space
            //HotKeyManager.RegisterHotKey(System.Windows.Forms.Keys.Space, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(System.Windows.Forms.Keys.Z, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += (s, a) => Toggle();

            // Minimize on focus lost
            Context.FocusGained += (s, a) => _log.Info("Gained focus");
            Context.FocusLost += (s, a) => { _log.Info("Lost focus"); Minimize(); };
        }

        public override void Draw()
        {
            if (Input.IsKeyPressed(Keys.Escape)) Minimize();

            ImGui.PushStyleVar(StyleVar.WindowRounding, 0);

            ImGui.SetNextWindowPos(Vector2.Zero, Condition.Always);
            ImGui.SetNextWindowSize(new Vector2(Context.Window.ClientBounds.Width, Context.Window.ClientBounds.Height), Condition.Always);

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
            if (Context.IsVisible) Minimize();
            else Maximize();
        }

        private void Maximize()
        {
            _log.Info("Maximize");

            Context.CenterWindowToDisplayWithMouse(Context.Window.Handle);

            Context.IsVisible = true;

            Context.Focus();
            _queryBox.Focus();
        }

        private void Minimize()
        {
            Task.Run(() =>
            {
                _log.Info("Minimize");

                _queryContext.Reset();

                Context.IsVisible = false;
            });
        }
    }
}