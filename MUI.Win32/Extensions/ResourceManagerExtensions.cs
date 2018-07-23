using MUI.Graphics;
using System;
using System.IO;

namespace MUI.Win32.Extensions
{
    public static class ResourceManagerExtensions
    {
        public static Image LoadImageFromIcon(this ResourceManager resourceManager, string path)
        {
            return resourceManager.LoadImage(path, textureLoader => new LazyImage(resourceManager.DefaultImage, () =>
            {
                try
                {
                    // Normalize path
                    path = Path.GetFullPath(path);

                    Console.WriteLine($"Loading image '{path}'...");

                    // Extract icon from the specified path
                    using (var icoExe = System.Drawing.Icon.ExtractAssociatedIcon(path))
                    using (var bmpStream = new MemoryStream())
                    {
                        // Convert to bitmap
                        icoExe.ToBitmap().Save(bmpStream, System.Drawing.Imaging.ImageFormat.Bmp);

                        // Convert to texture
                        return new Image(textureLoader.LoadTexture(bmpStream.ToArray()));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not load image '{path}': {ex.Message}");
                }

                return resourceManager.DefaultImage;
            }));
        }
    }
}