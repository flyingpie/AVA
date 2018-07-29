using ImGuiNET;
using ImGuiNET.FNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MUI.Extensions;
using MUI.Logging;
using SDL2;
using System;
using System.Threading;

namespace MUI
{
    public class UIContext : Game
    {
        public ResourceManager ResourceManager { get; set; }

        public event EventHandler FocusGained = delegate { };

        public event EventHandler FocusLost = delegate { };

        private GraphicsDeviceManager _graphics;
        private IImGuiRenderer _imGuiRenderer;

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

        private bool _wasActive;

        private UIBase _ui;

        public UIContext(int width, int height)
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferMultiSampling = true;

            _imGuiRenderer = new GuiXNAState(this);

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

        protected override void Update(GameTime gameTime)
        {
            if (IsActive && !_wasActive) FocusGained(this, EventArgs.Empty);
            if (!IsActive && _wasActive) FocusLost(this, EventArgs.Empty);

            if (!IsActive || !IsVisible)
            {
                SuppressDraw();

                Thread.Sleep(10);
            }

            Input.InputSnapshot.Update();

            _wasActive = IsActive;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _imGuiRenderer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            _ui.Draw();

            _imGuiRenderer.Render();

            base.Draw(gameTime);
        }
    }
}