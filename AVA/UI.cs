using AVA.Core;
using AVA.Core.QueryExecutors;
using AVA.Plugins.SysMon;
using ImGuiNET;
using MUI;
using MUI.DI;
using MUI.Logging;
using MUI.Win32;
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
        private bool _resetSelection = false;

        private IQueryExecutor _activeQueryExecutor;

        private bool _isConsoleShown = true;

        private ILog _log;

        public UI()
        {
            _log = Log.Get(this);
            _queryContext = new QueryContext();
        }

        public override void Load()
        {
            Maximize();

            // Toggle on Alt-Space
            HotKeyManager.RegisterHotKey(System.Windows.Forms.Keys.Z, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += (s, a) => Toggle();

            // Minimize on focus lost
            Context.FocusGained += (s, a) => _log.Info("Gained focus");
            Context.FocusLost += (s, a) => { _log.Info("Lost focus"); Minimize(); };

            HideConsole();
        }

        public override void Draw()
        {
            if (Input.IsKeyPressed(Keys.F12)) ToggleConsole();
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
                    _activeQueryExecutor?.Draw();
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

            // TODO: Make this a bit nicer
            //unsafe
            //{
            //    if (!_reset)
            //    {
            //        ImGui.InputText("query", _termBuffer, (uint)_termBuffer.Length, InputTextFlags.CallbackAlways, new TextEditCallback(data =>
            //        {
            //            if (_resetSelection)
            //            {
            //                data->CursorPos = _term.Length;

            //                data->SelectionStart = 0;
            //                data->SelectionEnd = 0;

            //                _resetSelection = false;
            //            }
            //            return 0;
            //        }));

            //        if (!ImGui.IsLastItemActive()) ImGui.SetKeyboardFocusHere();
            //    }
            //}

            ImGuiEx.InputText("query", _termBuffer, ref _reset, ref _resetSelection);

            if (_reset) _reset = false;

            if (_queryContext.Query != _term)
            {
                _termBuffer.CopyToBuffer(_queryContext.Query);

                _reset = true;
                _resetSelection = true;
            }

            if (Input.IsKeyPressed(Keys.Enter))
            {
                _log.Info($"ENTER");

                if (_activeQueryExecutor.TryExecute(_queryContext))
                {
                    if (_queryContext.HideUI) Minimize();

                    _queryContext.HideUI = true;
                }
            }

            var term = _termBuffer.BufferToString();
            if (_term != term)
            {
                _term = term;
                _activeQueryExecutor = QueryExecutorManager.GetQueryExecutor(term);

                _queryContext.Query = _term;
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

            Context.IsVisible = false;

            _termBuffer.ClearBuffer();

            _reset = true;
        }

        public void ToggleConsole()
        {
            if (_isConsoleShown) HideConsole();
            else ShowConsole();
        }

        public void ShowConsole()
        {
            PInvoke.ShowWindow(PInvoke.GetConsoleWindow(), PInvoke.SW_SHOW);

            _isConsoleShown = true;
        }

        public void HideConsole()
        {
            PInvoke.ShowWindow(PInvoke.GetConsoleWindow(), PInvoke.SW_HIDE);

            _isConsoleShown = false;
        }
    }
}