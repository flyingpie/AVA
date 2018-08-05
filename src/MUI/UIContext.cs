using ImGuiNET.FNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MUI.Extensions;
using MUI.Logging;
using SDL2;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MUI
{
    public class UIContext : Game
    {
        public ResourceManager ResourceManager { get; set; }

        public event EventHandler FocusGained = delegate { };

        public event EventHandler FocusLost = delegate { };

        private GraphicsDeviceManager _graphics;
        private ImGuiRenderer _imGuiRenderer;

        private bool _isVisible = true;

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (value) SDL.SDL_ShowWindow(Window.Handle);
                else SDL.SDL_HideWindow(Window.Handle);

                _isVisible = value;
            }
        }

        public float Opacity
        {
            set => SDL.SDL_SetWindowOpacity(Window.Handle, value);
        }

        private bool _wasActive;

        private UIBase _ui;

        public UIContext(int width, int height)
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferMultiSampling = true;

            _imGuiRenderer = new ImGuiRenderer(GraphicsDevice);

            ResourceManager = new ResourceManager(GraphicsDevice, _imGuiRenderer);
            Fonts.Load(ResourceManager);

            Window.IsBorderlessEXT = true;
            IsMouseVisible = true;
        }

        private ILog _sdlLog = Log.Get(typeof(SDL));

        public void CenterWindowToDisplayWithMouse(IntPtr windowHandle)
        {
            Sdl2Extensions.CenterWindowToDisplayWithMouse(Window.Handle);
        }

        public void Focus()
        {
            SDL.SDL_RaiseWindow(Window.Handle);
        }

        public void Run(UIBase ui)
        {
            _ui = ui;

            Run();
        }

        protected override void Initialize()
        {
            ResourceManager.Init();

            _imGuiRenderer.RebuildFontAtlas();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _ui.Load();

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            _ui.Unload();

            base.UnloadContent();
        }

        private Task _updateTask;

        protected override void Update(GameTime gameTime)
        {
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

            if (_updateTask == null)
                _updateTask = _ui.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Input.InputSnapshot.Update();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            _imGuiRenderer.BeforeLayout(gameTime);

            _ui.Draw();

            _imGuiRenderer.AfterLayout();

            base.Draw(gameTime);
        }
    }
}