using ImGuiNET.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MUI.Logging;
using MUI.Win32;
using MUI.Win32.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUI
{
    public class UIContext : Game
    {
        public static UIContext Instance { get; private set; }

        public ResourceManager ResourceManager { get; set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public event EventHandler FocusGained = delegate { };

        public event EventHandler FocusLost = delegate { };

        private GraphicsDeviceManager _graphics;
        private ImGuiRenderer _imGuiRenderer;

        public int FrameNumber { get; private set; }

        private Stack<UIBase> _uis;

        private Form _form;

        public bool IsVisible
        {
            get => _form.Visible;
            set => _form.InvokeIfRequired(() => _form.Visible = value);
        }

        public float Opacity
        {
            set
            {
                // TODO
            }
        }

        private bool _wasActive;

        public UIContext(int width, int height)
        {
            Instance = this;

            _uis = new Stack<UIBase>();

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferMultiSampling = true;

            Window.IsBorderless = true;
            IsMouseVisible = true;

            _form = Control.FromHandle(Window.Handle)?.FindForm() ?? throw new InvalidOperationException("Parent form not found");

        }

        public void CenterWindowToDisplayWithMouse()
        {
            var screen = Screen.AllScreens.FirstOrDefault(s => s.Bounds.Contains(Cursor.Position));

            var x = screen.Bounds.X + screen.Bounds.Width / 2 - _form.Size.Width / 2;
            var y = screen.Bounds.Y + screen.Bounds.Height / 2 - _form.Size.Height / 2;

            _form.Location = new System.Drawing.Point(x, y);
        }

        public void Focus()
        {
            PInvoke.SetForegroundWindow(Window.Handle);
        }

        public void HideFromTaskbar(bool hide)
        {
            if (hide)
            {
                PInvoke.SetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE, (PInvoke.GetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE) | PInvoke.WS_EX_TOOLWINDOW) & ~PInvoke.WS_EX_APPWINDOW);
            }
            else
            {
                PInvoke.SetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE, (PInvoke.GetWindowLong(Window.Handle, PInvoke.GWL_EX_STYLE) | PInvoke.WS_EX_TOOLWINDOW) & PInvoke.WS_EX_APPWINDOW);
            }
        }

        public void Resize(int width, int height)
        {
            if (_graphics.PreferredBackBufferHeight == width && _graphics.PreferredBackBufferHeight == height) return;

            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();

            CenterWindowToDisplayWithMouse();
        }

        public void PushUI(UIBase ui)
        {
            Resize(ui.Width, ui.Height);

            _uis.Push(ui);

            ui.Load();
        }

        public UIBase PopUI()
        {
            var ui = _uis.Pop();

            Resize(_uis.Peek().Width, _uis.Peek().Height);

            return ui;
        }

        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _imGuiRenderer = new ImGuiRenderer(this);

            ResourceManager = new ResourceManager(GraphicsDevice, _imGuiRenderer);
            Fonts.Load(ResourceManager);

            ResourceManager.Init();

            _imGuiRenderer.RebuildFontAtlas();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (_uis.Any())
                _uis.Peek().Load();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            if (_uis.Any())
                _uis.Peek().Unload();

            base.UnloadContent();
        }

        private Task _updateTask;

        protected override void Update(GameTime gameTime)
        {
            FrameNumber++;

            if (IsActive && !_wasActive) FocusGained(this, EventArgs.Empty);
            if (!IsActive && _wasActive) FocusLost(this, EventArgs.Empty);

            if (!IsActive || !IsVisible)
            {
                SuppressDraw();

                Thread.Sleep(10);
            }

            _wasActive = IsActive;

            if (_updateTask?.IsCompleted ?? false)
                _updateTask = null;

            if (_updateTask == null && _uis.Any())
                _updateTask = _uis.Peek().Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            Input.InputSnapshot.Update();
            _imGuiRenderer.BeforeLayout(gameTime);

            if (_uis.Any())
                _uis.Peek().Draw();

            SpriteBatch.End();

            _imGuiRenderer.AfterLayout();

            base.Draw(gameTime);
        }
    }
}