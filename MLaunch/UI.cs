using ImGuiNET;
using MLaunch.Core.QueryExecutors;
using MUI;
using MUI.DI;
using MUI.Extensions;
using MUI.Utils.Extensions;
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
        [Dependency] private QueryExecutorManager _queryExecutorManager;

        private IQueryExecutor _activeQueryExecutor;

        private byte[] _termBuffer = new byte[1024];
        private string _term;

        public override void Load()
        {
            Window.Width = 600;
            Window.Height = 300;
            Window.CenterToActiveMonitor();

            // Toggle on Alt-Space
            HotKeyManager.RegisterHotKey(Keys.Space, KeyModifiers.Alt | KeyModifiers.Shift);
            HotKeyManager.HotKeyPressed += (s, a) => { if (Window.Visible) { Minimize(); } else { Maximize(); } };

            // Minimize on focus lost
            Window.FocusLost += () => { Console.WriteLine("Lost focus"); Minimize(); };

            ////// TO DI ///////
            _queryExecutorManager = new QueryExecutorManager();
            _queryExecutorManager._resourceManager = ResourceManager;
            _queryExecutorManager.Initialize();
            ////// TO DI ///////
        }

        public override void Draw(UIContext context)
        {
            ImGui.PushStyleVar(StyleVar.WindowRounding, 0);

            ImGui.SetNextWindowPos(Vector2.Zero, Condition.Always, Vector2.Zero);
            ImGui.SetNextWindowSize(new Vector2(Window.Width, Window.Height), Condition.Always);

            ImGui.BeginWindow(string.Empty, WindowFlags.NoMove | WindowFlags.NoResize | WindowFlags.NoTitleBar);
            {
                ImGui.PushFont(context.Font24);

                DrawSearchBar(context);

                ImGui.Spacing();

                ImGui.BeginChild("query-executor", false, WindowFlags.Default);
                {
                    _activeQueryExecutor?.Draw(context);
                }
                ImGui.EndChild();

                ImGui.PopFont();
            }
            ImGui.EndWindow();
        }

        private void DrawSearchBar(UIContext context)
        {
            ImGui.PushFont(context.Font32);
            ImGui.PushItemWidth(-1);

            ImGui.InputText("query", _termBuffer, (uint)_termBuffer.Length, InputTextFlags.Default, null);

            if (context.Input.IsKeyDown(Key.Enter))
            {
                Console.WriteLine($"ENTER");

                if (_activeQueryExecutor.TryExecute(_term)) Minimize();
            }

            ImGui.SetKeyboardFocusHere();

            var term = _termBuffer.BufferToString();
            if (_term != term)
            {
                _term = term;
                _activeQueryExecutor = _queryExecutorManager.GetQueryExecutor(term);
            }

            ImGui.PopItemWidth();
            ImGui.PopFont();
        }

        private void Maximize()
        {
            Console.WriteLine("Maximize");

            Window.CenterToActiveMonitor();
            Window.Visible = true;

            PInvoke.SetForegroundWindow((int)Window.Handle);
        }

        private void Minimize()
        {
            Console.WriteLine("Minimize");

            Window.Visible = false;

            _termBuffer.ClearBuffer();
        }
    }
}