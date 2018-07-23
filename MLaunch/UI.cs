using ImGuiNET;
using Microsoft.CodeAnalysis.Scripting;
using MLaunch.Core.QueryExecutors;
using MLaunch.Plugins.SysMon;
using MUI;
using MUI.DI;
using MUI.Extensions;
using MUI.Scripting;
using MUI.Win32;
using MUI.Win32.Extensions;
using System;
using System.Linq;
using System.Numerics;

//using System.Windows.Forms;
using Veldrid;

namespace MLaunch
{
    public class UI : UIBase
    {
        [Dependency] public UIContext Context { get; set; }
        [Dependency] private IQueryExecutorManager QueryExecutorManager { get; set; }

        [Dependency] private SysMonService SysMon { get; set; }

        private byte[] _termBuffer = new byte[1024];
        private string _term;

        private IQueryExecutor _activeQueryExecutor;

        public override void Load()
        {
            Context.Window.Width = 600;
            Context.Window.Height = 300;
            Context.Window.CenterToActiveMonitor();

            // Toggle on Alt-Space
            //HotKeyManager.RegisterHotKey(Keys.Space, KeyModifiers.Alt | KeyModifiers.Shift);
            //HotKeyManager.HotKeyPressed += (s, a) => Toggle();

            // Minimize on focus lost
            //Context.Window.FocusLost += () => { Console.WriteLine("Lost focus"); Minimize(); };

            _scriptHost = new ScriptHost("Resources/Layouts/Default/Default.csx");
            //_scriptHost.Interop.Services.Add(() => Context);
            _scriptHost.Interop.Add(() => Context);
            _scriptHost.Interop.Add(() => SysMon);
            _scriptHost.Compile();
        }

        private ScriptHost _scriptHost;

        public override void Draw()
        {
            if (Context.Input.IsKeyDown(Key.F5))
            {
                try
                {
                    _scriptHost.Compile().GetAwaiter().GetResult();
                }
                catch (CompilationErrorException ex)
                {
                    Console.WriteLine("Error while compiling script:");
                    ex.Diagnostics.ToList().ForEach(e => Console.WriteLine(e.ToString()));
                }
            }

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
                ImGui.BeginChild("query-executor", new Vector2(ImGui.GetWindowContentRegionWidth(), ImGui.GetContentRegionAvailable().Y - 25), false, WindowFlags.Default);
                {
                    _activeQueryExecutor?.Draw();
                }
                ImGui.EndChild();

                // Footer
                ImGui.PushFont(Context.Font16);
                ImGui.BeginChild("footer", false, WindowFlags.Default);
                {
                    //    // SysMon
                    //    ImGui.Text($"CPU {SysMon.CpuUsage.ToString("0.00")}");

                    //    ImGui.SameLine();
                    //    ImGui.Text($"Mem {SysMon.MemUsage.ToString("0.00")}");

                    //    foreach (var drive in SysMon.Drives)
                    //    {
                    //        ImGui.SameLine();
                    //        ImGui.Text($"{drive.Name} {drive.Usage.ToString("0.00")}");
                    //    }
                    //    ////////////////////

                    _scriptHost.Run().GetAwaiter().GetResult();
                }
                ImGui.EndChild();
                ImGui.PopFont();

                ImGui.PopFont();
            }
            ImGui.EndWindow();
        }

        private void DrawSearchBar()
        {
            ImGui.PushFont(Context.Font32);
            ImGui.PushItemWidth(-1);

            ImGui.InputText("query", _termBuffer, (uint)_termBuffer.Length, InputTextFlags.EnterReturnsTrue, null);

            if (Context.Input.IsKeyDown(Key.Enter))
            {
                Console.WriteLine($"ENTER");

                if (_activeQueryExecutor.TryExecute(_term)) Minimize();
            }

            ImGui.SetKeyboardFocusHere();

            var term = _termBuffer.BufferToString();
            if (_term != term)
            {
                _term = term;
                _activeQueryExecutor = QueryExecutorManager.GetQueryExecutor(term);
            }

            ImGui.PopItemWidth();
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
    }
}