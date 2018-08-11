using Microsoft.Xna.Framework.Graphics;
using MUI.ImGuiEx;
using System;
using System.IO;

namespace MUI.Graphics
{
    public class TextureLoader
    {
        private GraphicsDevice _graphicsDevice;
        private ImGuiRenderer _imGuiRenderer;

        public TextureLoader(GraphicsDevice graphicsDevice, ImGuiRenderer imGuiRenderer)
        {
            _graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            _imGuiRenderer = imGuiRenderer ?? throw new ArgumentNullException(nameof(imGuiRenderer));
        }

        public Image Load(Stream stream)
        {
            var tex2d = Texture2D.FromStream(_graphicsDevice, stream);
            var pointer = _imGuiRenderer.BindTexture(tex2d);

            return new Image(pointer, tex2d.Width, tex2d.Height);
        }

        public Image Load(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return Load(stream);
            }
        }
    }
}