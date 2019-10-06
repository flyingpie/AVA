using MUI.Graphics;
using MUI.Logging;
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
                var _log = Log.Get<ResourceManager>();

                try
                {
                    // Normalize path
                    path = Path.GetFullPath(path);

                    _log.Info($"Loading image from icon at path '{path}'...");

                    using (var bmpStream = new MemoryStream())
                    {
                        var bmp = ThumbnailLoader.GetThumbnail(path, 50, 50, ThumbnailOptions.None);
                        bmp.Save(bmpStream, System.Drawing.Imaging.ImageFormat.Bmp);

                        // Convert to texture
                        return textureLoader.Load(bmpStream.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Could not load image from icon: '{ex.Message}'.", ex);
                    return ResourceManager.Instance.DefaultImage;
                }
            }));
        }

        public static Image LoadImageFromIcon(this ResourceManager resourceManager, string cacheKey, System.Drawing.Icon icon)
        {
            return resourceManager.LoadImage(cacheKey, textureLoader => new LazyImage(resourceManager.DefaultImage, () =>
            {
                var _log = Log.Get<ResourceManager>();

                try
                {
                    _log.Info($"Loading image from icon...");

                    using (var bmpStream = new MemoryStream())
                    {
                        // Convert to bitmap
                        icon.ToBitmap().Save(bmpStream, System.Drawing.Imaging.ImageFormat.Bmp);

                        // Convert to texture
                        return textureLoader.Load(bmpStream.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Could not load image from icon: '{ex.Message}'.", ex);
                    return ResourceManager.Instance.DefaultImage;
                }
            }));
        }

        public static Image LoadImageFromBitmap(this ResourceManager resourceManager, string cacheKey, System.Drawing.Image image)
        {
            return resourceManager.LoadImage(cacheKey, textureLoader => new LazyImage(resourceManager.DefaultImage, () =>
            {
                var _log = Log.Get<ResourceManager>();
                try
                {
                    _log.Info($"Loading image from System.Drawing.Image...");

                    using (var bmpStream = new MemoryStream())
                    {
                        image.Save(bmpStream, System.Drawing.Imaging.ImageFormat.Bmp);

                        // Convert to texture
                        return textureLoader.Load(bmpStream.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Could not load image from icon: '{ex.Message}'.", ex);
                    return ResourceManager.Instance.DefaultImage;
                }
            }));
        }
    }
}