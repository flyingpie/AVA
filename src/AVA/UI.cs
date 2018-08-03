using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Plugins.SysMon;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.Logging;
using MUI.Win32.Input;
using System;
using System.Numerics;

namespace AVA
{
    public class UI : UIBase
    {
        [Dependency] public UIContext Context { get; set; }

        [Dependency] private IQueryExecutorManager QueryExecutorManager { get; set; }

        [Dependency] private SysMonService SysMon { get; set; }

        private QueryContext _queryContext;

        private byte[] _termBuffer = new byte[1024];
        private string _term;

        private bool _reset = false;
        private int _id;

        private ILog _log;

        public UI()
        {
            _log = Log.Get(this);
            _queryContext = new QueryContext();
        }

        public override void Load()
        {
            SDL2.SDL.SDL_SetWindowOpacity(Context.Window.Handle, .9f);

            Maximize();

            // Toggle on Alt-Space
            HotKeyManager.RegisterHotKey(System.Windows.Forms.Keys.Space, KeyModifiers.Alt);
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

                //// Footer
                DrawFooter();

                ImGui.PopFont();
            }
            ImGui.EndWindow();
        }

        private void DrawSearchBar()
        {
            ImGui.PushFont(Fonts.Regular32);
            ImGui.PushItemWidth(-1);

            var isQcUpdated = false;

            // Update input buffer based on the query context
            if (_term != _queryContext.Text)
            {
                _termBuffer.CopyToBuffer(_queryContext.Text);
                _term = _queryContext.Text;

                _reset = true;
                _id++;

                isQcUpdated = true;
            }

            ImGuiEx.InputText(_id, _termBuffer, ref _reset);

            // Execute when ENTER was pressed
            if (Input.IsKeyPressed(Keys.Enter))
            {
                _log.Info($"ENTER");

                if (QueryExecutorManager.TryExecute(_queryContext))
                {
                    if (_queryContext.HideUI) Minimize();

                    if (_queryContext.ResetText) _queryContext.Reset();
                }
            }

            // Update the query context if the input buffer was changed
            var term = _termBuffer.BufferToString();
            if (_term != term || isQcUpdated)
            {
                _term = term;
                _queryContext.Text = term;

                QueryExecutorManager.TryHandle(_queryContext);
            }

            ImGui.PopItemWidth();
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

            Context.IsVisible = true;

            Context.CenterWindowToDisplayWithMouse(Context.Window.Handle);
            Context.Focus();
        }

        private void Minimize()
        {
            _log.Info("Minimize");

            _queryContext.Reset();

            Context.IsVisible = false;
        }
    }
}