using ImGuiNET;
using System.Numerics;
using System.Threading;
using Veldrid;
using Veldrid.Sdl2;

namespace MUI
{
    public class UIContext
    {
        public Font Font32 { get; set; }

        public Font Font24 { get; set; }

        public Font Font16 { get; set; }

        public InputSnapshot Input { get; set; }

        public Vector3 ClearColor { get; set; } = new Vector3(0.45f, 0.55f, 0.6f);

        public ResourceManager ResourceManager { get; private set; }

        public Sdl2Window Window => _window;

        private CommandList _cl;
        private ImGuiController _controller;
        private GraphicsDevice _graphicsDevice;
        private Sdl2Window _window;

        public UIContext()
        {
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(0, 0, 800, 600),
                new GraphicsDeviceOptions(true, null, true),
                out _window,
                out _graphicsDevice);

            _cl = _graphicsDevice.ResourceFactory.CreateCommandList();

            _controller = new ImGuiController(_graphicsDevice, _graphicsDevice.MainSwapchain.Framebuffer.OutputDescription, Window.Width, Window.Height);

            ResourceManager = new ResourceManager(_graphicsDevice, _controller);

            // TODO: Move
            Font16 = ResourceManager.LoadFont(@"Resources\Fonts\OpenSans\OpenSans-Light.ttf", 16);
            Font24 = ResourceManager.LoadFont(@"Resources\Fonts\OpenSans\OpenSans-Light.ttf", 24);
            Font32 = ResourceManager.LoadFont(@"Resources\Fonts\OpenSans\OpenSans-Light.ttf", 32);

            _controller.Initialize();
            _controller.PostInit();

            ResourceManager.Init();
        }

        public int Run(UIBase ui)
        {
            ui.Load();

            while (Window.Exists)
            {
                if (!Window.Exists) { break; }

                InputSnapshot snapshot = Window.PumpEvents();
                Input = snapshot;

                _controller.Update(1f / 60f, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.

                if (Window.Visible)
                {
                    ui.Draw();

                    _cl.Begin();
                    _cl.SetFramebuffer(_graphicsDevice.MainSwapchain.Framebuffer);
                    _cl.ClearColorTarget(0, new RgbaFloat(ClearColor.X, ClearColor.Y, ClearColor.Z, 1f));
                    _controller.Render(_graphicsDevice, _cl);
                    _cl.End();
                    _graphicsDevice.SubmitCommands(_cl);
                    _graphicsDevice.SwapBuffers(_graphicsDevice.MainSwapchain);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }

            ui.Unload();

            // Clean up Veldrid resources
            _graphicsDevice.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _graphicsDevice.Dispose();

            return 0;
        }
    }
}