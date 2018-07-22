using ImGuiNET;
using MUI.Graphics;
using System;
using System.Collections.Concurrent;
using Veldrid;
using Veldrid.ImageSharp;

namespace MUI
{
    public class ResourceManager
    {
        public Image DefaultImage { get; private set; }

        private GraphicsDevice _graphicsDevice;
        private ImGuiController _controller;
        private TextureLoader _textureLoader;

        private ConcurrentDictionary<string, Image> _loadedImages;

        public ResourceManager(GraphicsDevice graphicsDevice, ImGuiController controller)
        {
            _graphicsDevice = graphicsDevice;
            _controller = controller;
            _textureLoader = new TextureLoader(graphicsDevice, controller);

            _loadedImages = new ConcurrentDictionary<string, Image>();
        }

        //[RunAfterInject]
        public /*private*/ void Init()
        {
            //DefaultImage = new Image(_textureLoader.LoadTexture(@"Resources\Images\crashlog-doom.png"));

            DefaultImage = LoadImage(@"Resources\Images\crashlog-doom.png");
        }

        public Font LoadFont(string path, int pixelSize)
        {
            return ImGui.GetIO().FontAtlas.AddFontFromFileTTF(path, pixelSize);
        }

        public Image LoadImage(string path)
        {
            //return _loadedImages.GetOrAdd(path, key => new Image(LoadTexture(path)));

            return LoadImage(path, loader => new Image(loader.LoadTexture(path)));
        }

        public Image LoadImage(string cacheKey, byte[] data)
        {
            //return _loadedImages.GetOrAdd(cacheKey, key => new Image(LoadTexture(data)));

            return LoadImage(cacheKey, loader => new Image(loader.LoadTexture(data)));
        }

        public Image LoadImage(string cacheKey, Func<TextureLoader, Image> factory)
        {
            return _loadedImages.GetOrAdd(cacheKey, key => factory(_textureLoader));
        }

        #region Low-level

        //public IntPtr LoadTexture(string path)
        //{
        //    return LoadTexture(new ImageSharpTexture(path));
        //}

        //public IntPtr LoadTexture(byte[] data)
        //{
        //    var image = SixLabors.ImageSharp.Image.Load(data);

        //    return LoadTexture(new ImageSharpTexture(image));
        //}

        //public IntPtr LoadTexture(ImageSharpTexture imageSharpTexture)
        //{
        //    lock (_lock)
        //    {
        //        var texture = imageSharpTexture.CreateDeviceTexture(_graphicsDevice, _graphicsDevice.ResourceFactory);

        //        return _controller.GetOrCreateImGuiBinding(_graphicsDevice.ResourceFactory, texture);
        //    }
        //}

        #endregion Low-level
    }

    public class TextureBinding
    {
        public ImageSharpTexture IST { get; set; }

        public Texture Texture { get; set; }

        public IntPtr Pointer { get; set; }
    }
}