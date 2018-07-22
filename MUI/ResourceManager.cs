using ImGuiNET;
using System;
using System.Collections.Generic;
using Veldrid;
using Veldrid.ImageSharp;

namespace MUI
{
    public class ResourceManager
    {
        private GraphicsDevice _graphicsDevice;
        private ImGuiController _controller;

        private readonly Dictionary<string, TextureBinding> _textures
            = new Dictionary<string, TextureBinding>();

        public ResourceManager(GraphicsDevice graphicsDevice, ImGuiController controller)
        {
            _graphicsDevice = graphicsDevice;
            _controller = controller;
        }

        public Font LoadFont(string path, int pixelSize)
        {
            return ImGui.GetIO().FontAtlas.AddFontFromFileTTF(path, pixelSize);
        }

        public IntPtr LoadTexture(string path)
        {
            if (_textures.ContainsKey(path)) return _textures[path].Pointer;

            var ist = new ImageSharpTexture(path);

            var texture = ist.CreateDeviceTexture(_graphicsDevice, _graphicsDevice.ResourceFactory);

            var pointer = _controller.GetOrCreateImGuiBinding(_graphicsDevice.ResourceFactory, texture);

            _textures.Add(path, new TextureBinding()
            {
                IST = ist,
                Texture = texture,
                Pointer = pointer
            });

            return pointer;
        }

        public IntPtr LoadTexture(byte[] data)
        {
            var image = SixLabors.ImageSharp.Image.Load(data);

            var ist = new ImageSharpTexture(image, false);

            var texture = ist.CreateDeviceTexture(_graphicsDevice, _graphicsDevice.ResourceFactory);

            var pointer = _controller.GetOrCreateImGuiBinding(_graphicsDevice.ResourceFactory, texture);

            return pointer;
        }
    }

    public class TextureBinding
    {
        public ImageSharpTexture IST { get; set; }

        public Texture Texture { get; set; }

        public IntPtr Pointer { get; set; }
    }
}