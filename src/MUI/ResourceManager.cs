using ImGuiNET;
using ImGuiNET.FNA;
using Microsoft.Xna.Framework.Graphics;
using MUI.Graphics;
using MUI.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;

namespace MUI
{
    public class ResourceManager
    {
        public Image DefaultImage { get; private set; }

        public Image LoadingImage { get; private set; }

        private ConcurrentDictionary<string, Image> _loadedImages;
        private TextureLoader _textureLoader;

        private GraphicsDevice _graphicsDevice;
        private ImGuiRenderer _imGuiRenderer;

        private ILog _log;

        public ResourceManager(GraphicsDevice graphicsDevice, ImGuiRenderer imGuiRenderer)
        {
            _log = Log.Get(this);

            _loadedImages = new ConcurrentDictionary<string, Image>();
            _textureLoader = new TextureLoader(graphicsDevice, imGuiRenderer);

            _graphicsDevice = graphicsDevice;
            _imGuiRenderer = imGuiRenderer;
        }

        public void Init()
        {
            // TODO: Move this
            DefaultImage = LoadImage("Resources/Images/default-image.png");
            LoadingImage = new AnimatedImage(LoadImage("Resources/Images/loading-image.png"), 9, 5, 40, Direction.TopToBottom, 5);

            var x = 2;
        }

        public Font LoadFont(string path, int pixelSize)
        {
            var font = ImGui.GetIO().FontAtlas.AddFontFromFileTTF(path, pixelSize);

            _imGuiRenderer.RebuildFontAtlas();

            return font;
        }

        public Image LoadImageFromUrl(string url)
        {
            return LoadImage(url, loader => new LazyImage(DefaultImage, () =>
            {
                var response = new HttpClient().GetAsync(url).Result;
                var data = response.Content.ReadAsByteArrayAsync().Result;

                return loader.Load(data);
            }));
        }

        // TODO: Add size cap
        public Image LoadImage(string path)
        {
            return LoadImage(path, File.ReadAllBytes(path));
        }

        // TODO: Add size cap
        public Image LoadImage(string cacheKey, byte[] data)
        {
            return LoadImage(cacheKey, factory => factory.Load(data));
        }

        public Image LoadImage(string cacheKey, Func<TextureLoader, Image> factory)
        {
            var image = _loadedImages.GetOrAdd(cacheKey, key => factory(_textureLoader));

            image.Initialize();

            return image;
        }
    }
}