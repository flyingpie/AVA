using ImGuiNET;
using MLaunch.Core;
using MLaunch.Core.QueryExecutors;
using MLaunch.Plugins.SysMon;
using MUI;
using MUI.DI;
using MUI.Extensions;
using MUI.Win32;
using MUI.Win32.Extensions;
using MUI.Win32.Input;
using System;
using System.Numerics;
using System.Windows.Forms;
using Veldrid;

namespace MLaunch
{
    public class UI : UIBase
    {
        [Dependency] public UIContext Context { get; set; }

        [Dependency] private IQueryExecutorManager QueryExecutorManager { get; set; }

        [Dependency] private SysMonService SysMon { get; set; }

        private QueryContext _queryContext;

        private byte[] _termBuffer = new byte[1024];
        private string _term;

        private IQueryExecutor _activeQueryExecutor;

        private bool _isConsoleShown = true;

        public UI()
        {
            _queryContext = new QueryContext();
        }

        public override void Load()
        {
            Context.Window.Width = 600;
            Context.Window.Height = 300;
            Context.Window.CenterToActiveMonitor();

            // Toggle on Alt-Space
            HotKeyManager.RegisterHotKey(Keys.Space, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += (s, a) => Toggle();

            // Minimize on focus lost
            Context.Window.FocusLost += () => { Console.WriteLine("Lost focus"); Minimize(); };

            HideConsole();
        }

        public override void Update()
        {
        }

        public override void Draw()
        {
            if (Context.Input.IsKeyDown(Key.F12)) ToggleConsole();
            if (Context.Input.IsKeyDown(Key.Escape)) Minimize();

            ImGui.PushStyleVar(StyleVar.WindowRounding, 0);

            ImGui.SetNextWindowPos(Vector2.Zero, Condition.Always, Vector2.Zero);
            ImGui.SetNextWindowSize(new Vector2(Context.Window.Width, Context.Window.Height), Condition.Always);

            ImGui.BeginWindow(string.Empty, WindowFlags.NoMove | WindowFlags.NoResize | WindowFlags.NoTitleBar);
            {
                // Search bar
                ImGui.PushFont(Context.Font24);

                DrawSearchBar();

                ImGui.Spacing();

                // Query executor
                ImGui.BeginChild("query-executor", new Vector2(ImGui.GetWindowContentRegionWidth(), ImGui.GetContentRegionAvailable().Y - 20), false, WindowFlags.Default);
                {
                    _activeQueryExecutor?.Draw();
                }
                ImGui.EndChild();

                // Footer
                DrawFooter();

                ImGui.PopFont();
            }
            ImGui.EndWindow();
        }

        private byte[] _termBuffer2 = new byte[100];

        private bool _off = false;
        private bool _resetSelection = false;

        private void DrawSearchBar()
        {
            ImGui.PushFont(Context.Font32);
            ImGui.PushItemWidth(-1);

            // TODO: Make this a bit nicer
            unsafe
            {
                if (!_off)
                {
                    ImGui.InputText("query", _termBuffer, (uint)_termBuffer.Length, InputTextFlags.CallbackCompletion | InputTextFlags.CallbackAlways, new TextEditCallback(data =>
                    {
                        if (_resetSelection)
                        {
                            data->CursorPos = _term.Length;

                            data->SelectionStart = 0;
                            data->SelectionEnd = 0;

                            _resetSelection = false;
                        }
                        return 0;
                    }));

                    if (!ImGui.IsLastItemActive()) ImGui.SetKeyboardFocusHere();
                }
            }

            if (_off) _off = false;

            if (_queryContext.Query != _term)
            {
                _termBuffer.CopyToBuffer(_queryContext.Query);

                _off = true;
                _resetSelection = true;
            }

            if (Context.Input.IsKeyDown(Key.Enter))
            {
                Console.WriteLine($"ENTER");

                if (_activeQueryExecutor.TryExecute(_queryContext))
                {
                    if (_queryContext.HideUI) Minimize();

                    _queryContext.HideUI = true;
                }
            }

            //ImGui.Text("Ac: " + ImGui.IsAnyItemActive());
            //ImGui.SetKeyboardFocusHere();

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
            ImGui.PushFont(Context.Font16);
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
                ////////////////////
            }
            ImGui.EndChild();
            ImGui.PopFont();
        }

        private void Toggle()
        {
            if (Context.Window.Visible) Minimize();
            else Maximize();
        }

        private void Maximize()
        {
            Console.WriteLine("Maximize");

            Context.Window.CenterToActiveMonitor();
            Context.Window.Visible = true;

            PInvoke.SetForegroundWindow((int)Context.Window.Handle);
        }

        private void Minimize()
        {
            Console.WriteLine("Minimize");

            Context.Window.Visible = false;

            _termBuffer.ClearBuffer();
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