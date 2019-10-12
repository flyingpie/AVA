using AVA.Core;
using AVA.Core.Footers;
using AVA.Core.QueryExecutors;
using AVA.Core.Settings;
using ImGuiNET;
using Microsoft.Xna.Framework.Graphics;
using MUI;
using MUI.DI;
using MUI.ImGuiControls;
using MUI.Logging;
using MUI.Win32;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace AVA
{
    public class UI : UIBase
    {
        [Dependency] public AVASettings Settings { get; set; }

        [Dependency] private IQueryExecutorManager QueryExecutorManager { get; set; }

        [Dependency] public IFooter[] Footers { get; set; }

        private UIContext _uic;
        private QueryContext _queryContext;
        private ILog _log;

        private TextBox _queryBox;

        private Texture2D _bg;

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
            _uic.HideFromTaskbar(true);

            _uic.Opacity = .9f;

            Maximize();

            // Toggle on Alt-Space
            HotKeyManager.RegisterHotKey(Settings.ToggleUIKey, Settings.ToggleUIKeyModifiers);
            HotKeyManager.HotKeyPressed += (s, a) => Toggle();

            // Minimize on focus lost
            _uic.FocusGained += (s, a) => _log.Info("Gained focus");
            _uic.FocusLost += (s, a) => { _log.Info("Lost focus"); Minimize(); };

            // Background image
            if (!string.IsNullOrWhiteSpace(Settings.BackgroundImage))
            {
                string bgImage = null;

                try
                {
                    bgImage = Environment.ExpandEnvironmentVariables(Settings.BackgroundImage);
                    _bg = Texture2D.FromStream(_uic.GraphicsDevice, File.OpenRead(bgImage));
                }
                catch (Exception ex)
                {
                    _log.Error($"Could not load background image at '{bgImage}': {ex.Message}.", ex);
                }
            }

            // TODO: Make configurable
            var style = ImGui.GetStyle();
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(.2f, .2f, .2f, .5f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0f, 0f, 0f, .8f);
        }

        public override void Draw()
        {
            if (Input.IsKeyPressed(Keys.Escape)) Minimize();

            if (_bg != null)
            {
                _uic.SpriteBatch.Draw(_bg, new Microsoft.Xna.Framework.Rectangle(0, 0, Width, Height), Microsoft.Xna.Framework.Color.White);
            }

            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);

            ImGui.SetNextWindowPos(Vector2.Zero, ImGuiCond.Always);
            ImGui.SetNextWindowSize(new Vector2(_uic.Window.ClientBounds.Width, _uic.Window.ClientBounds.Height), ImGuiCond.Always);

            ImGui.Begin(string.Empty, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar);
            {
                // Search bar
                ImGui.PushFont(Fonts.Regular24);

                DrawSearchBar();

                ImGui.Spacing();

                // Query executor
                ImGui.BeginChild("query-executor", new Vector2(ImGui.GetWindowContentRegionWidth(), ImGui.GetContentRegionAvail().Y - 20), false, ImGuiWindowFlags.None);
                {
                    QueryExecutorManager.Draw();
                }
                ImGui.EndChild();

                // Footer
                Footers?.OrderBy(f => f.Priority).FirstOrDefault()?.Draw();

                ImGui.PopFont();
            }
            ImGui.End();
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