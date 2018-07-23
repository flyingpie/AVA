using System;
using System.Runtime.CompilerServices;
using Veldrid;
using Veldrid.Sdl2;

namespace MUI
{
    public static class VeldridStartup
    {
        public static void CreateWindowAndGraphicsDevice(
            WindowCreateInfo windowCI,
            GraphicsDeviceOptions deviceOptions,
            out Sdl2Window window,
            out GraphicsDevice gd)
        {
            Sdl2Native.SDL_Init(SDLInitFlags.Video);

            window = CreateWindow(ref windowCI);
            gd = CreateVulkanGraphicsDevice(deviceOptions, window);
        }

        public static Sdl2Window CreateWindow(ref WindowCreateInfo windowCI)
        {
               SDL_WindowFlags flags =
                /*SDL_WindowFlags.Borderless
              |*/ SDL_WindowFlags.OpenGL
              | SDL_WindowFlags.Shown
              /*| SDL_WindowFlags.SkipTaskbar*/;

            Sdl2Window window = new Sdl2Window(
                "MLaunch",
                windowCI.X,
                windowCI.Y,
                windowCI.WindowWidth,
                windowCI.WindowHeight,
                flags,
                false);

            return window;
        }

        public static unsafe GraphicsDevice CreateVulkanGraphicsDevice(GraphicsDeviceOptions options, Sdl2Window window)
        {
            SwapchainDescription scDesc = new SwapchainDescription(
                GetSwapchainSource(window),
                (uint)window.Width,
                (uint)window.Height,
                options.SwapchainDepthFormat,
                options.SyncToVerticalBlank);
            GraphicsDevice gd = GraphicsDevice.CreateVulkan(options, scDesc);

            return gd;
        }

        public static unsafe SwapchainSource GetSwapchainSource(Sdl2Window window)
        {
            IntPtr sdlHandle = window.SdlWindowHandle;
            SDL_SysWMinfo sysWmInfo;
            Sdl2Native.SDL_GetVersion(&sysWmInfo.version);
            Sdl2Native.SDL_GetWMWindowInfo(sdlHandle, &sysWmInfo);
            switch (sysWmInfo.subsystem)
            {
                case SysWMType.Windows:
                    Win32WindowInfo w32Info = Unsafe.Read<Win32WindowInfo>(&sysWmInfo.info);
                    return SwapchainSource.CreateWin32(w32Info.Sdl2Window, w32Info.hinstance);

                default:
                    throw new PlatformNotSupportedException("Cannot create a SwapchainSource for " + sysWmInfo.subsystem + ".");
            }
        }
    }
}