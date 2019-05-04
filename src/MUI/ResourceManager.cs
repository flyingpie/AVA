using ImGuiNET;
using ImGuiNET.XNA;
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
        public static ResourceManager Instance { get; private set; }

        public Image DefaultImage { get; private set; }

        public Image LoadingImage { get; private set; }

        public Image WhiteImage { get; private set; }

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

            Instance = this;
        }

        public void Init()
        {
            // TODO: Move this
            DefaultImage = _textureLoader.Load(File.ReadAllBytes("Resources/Images/default-image.png"));
            LoadingImage = new AnimatedImage(_textureLoader.Load(File.ReadAllBytes("Resources/Images/loading-image.png")), 9, 5, 40, Direction.TopToBottom, 5);

            var transp = new System.Drawing.Bitmap(1, 1);
            System.Drawing.Graphics.FromImage(transp).Clear(System.Drawing.Color.White);
            WhiteImage = _textureLoader.Load(transp.ToByteArray());
        }

        public Font LoadFont(string path, int pixelSize)
        {
            var font = ImGui.GetIO().FontAtlas.AddFontFromFileTTF(path, pixelSize);

            _imGuiRenderer.RebuildFontAtlas();

            return font;
        }

        public Image LoadGlyph(string glyph, System.Drawing.FontFamily fontFamily, int fontSizeEm)
        {
            return LoadImage($"glyph_{fontFamily.Name}_{fontSizeEm}_{glyph}", loader =>
            {
                var img = GlyphRenderer.RenderGlyph(glyph, fontFamily, fontSizeEm, System.Drawing.Color.White);

                return loader.Load(img.ToByteArray());
            });
        }

        public Image LoadImageFromUrl(string url)
        {
            return LoadImage(url, loader => new LazyImage(DefaultImage, async () =>
            {
                var response = await new HttpClient().GetAsync(url);
                var data = await response.Content.ReadAsByteArrayAsync();

                return loader.Load(data);
            }));
        }

        public bool TryLoadImage(string path, out Image image)
        {
            image = null;

            if (string.IsNullOrWhiteSpace(path)) return false;

            try
            {
                image = LoadImage(path);
                return true;
            }
            catch
            {
                // TODO: LOG
                return false;
            }
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