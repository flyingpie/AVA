using System;
using Veldrid;
using Veldrid.ImageSharp;

namespace MUI.Graphics
{
    public class TextureLoader
    {
        private GraphicsDevice _graphicsDevice;
        private ImGuiController _controller;

        private object _lock = new object();

        public TextureLoader(GraphicsDevice graphicsDevice, ImGuiController controller)
        {
            _graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
        }

        public IntPtr LoadTexture(string path)
        {
            return LoadTexture(new ImageSharpTexture(path));
        }

        public IntPtr LoadTexture(byte[] data)
        {
            var image = SixLabors.ImageSharp.Image.Load(data);

            return LoadTexture(new ImageSharpTexture(image));
        }

        public IntPtr LoadTexture(ImageSharpTexture imageSharpTexture)
        {
            lock (_lock)
            {
                var texture = imageSharpTexture.CreateDeviceTexture(_graphicsDevice, _graphicsDevice.ResourceFactory);

                return _controller.GetOrCreateImGuiBinding(_graphicsDevice.ResourceFactory, texture);
            }
        }
    }
}