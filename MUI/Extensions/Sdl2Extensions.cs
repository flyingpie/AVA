using MUI.Logging;
using SDL2;
using System;

namespace MUI.Extensions
{
    public static class Sdl2Extensions
    {
        private static ILog _log;

        static Sdl2Extensions()
        {
            _log = Log.Get(typeof(SDL));
        }

        public static void CenterWindowToDisplayWithMouse(IntPtr windowHandle)
        {
            // Get mouse position across all displays
            if (SDL.SDL_GetGlobalMouseState(out int mouseX, out int mouseY) != 0) { _log.Error("Could not get global mouse state"); }
            SDL.SDL_Point mouse = new SDL.SDL_Point() { x = mouseX, y = mouseY };

            // Get all display bounds
            var numDisplays = SDL.SDL_GetNumVideoDisplays();
            if (numDisplays == 0) _log.Error("Could not get num video displays");

            //var displays = new SDL.SDL_Rect[numDisplays];

            //for (int displayIndex = 0; displayIndex < numDisplays; displayIndex++)
            //{
            //    if (SDL.SDL_GetCurrentDisplayMode(displayIndex, out var mode) != 0) { _log.Error($"Could not get current display mode for display {displayIndex}"); }
            //    if (SDL.SDL_GetDisplayBounds(displayIndex, out var dpBounds) != 0) { _log.Error($"Could not get display bounds for display {displayIndex}"); }

            //    displays[displayIndex] = dpBounds;
            //}

            //var displayWithMouse = displays[0];

            //// Get display with mouse
            //for (int i = 0; i < displays.Length; i++)
            //{
            //    //if ((mouseX > b.x && mouseX < b.x + b.w) && (mouseY > b.y && mouseY < b.y + b.h))
            //    if (SDL.SDL_PointInRect(ref mouse, ref displays[i]) == SDL.SDL_bool.SDL_TRUE)
            //    {
            //        displayWithMouse = displays[i];
            //    }
            //}

            //var displays = new SDL.SDL_Rect[numDisplays];

            var displayWithMouse = new SDL.SDL_Rect();

            for (int displayIndex = 0; displayIndex < numDisplays; displayIndex++)
            {
                if (SDL.SDL_GetCurrentDisplayMode(displayIndex, out var mode) != 0) { _log.Error($"Could not get current display mode for display {displayIndex}"); }
                if (SDL.SDL_GetDisplayBounds(displayIndex, out var dpBounds) != 0) { _log.Error($"Could not get display bounds for display {displayIndex}"); }

                if (SDL.SDL_PointInRect(ref mouse, ref dpBounds) == SDL.SDL_bool.SDL_TRUE)
                {
                    displayWithMouse = dpBounds;
                    break;
                }
            }

            // Get window size
            SDL.SDL_GetWindowSize(windowHandle, out var windowWidth, out var windowHeight);

            // Get mouse display center
            var x = displayWithMouse.x + (displayWithMouse.w / 2) - (windowWidth / 2);
            var y = displayWithMouse.y + (displayWithMouse.h / 2) - (windowHeight / 2);

            SDL.SDL_SetWindowPosition(windowHandle, x, y);
        }
    }
}