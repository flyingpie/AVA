using ImGuiNET;
using System;
using System.Numerics;

namespace MUI.Graphics
{
    public class Image
    {
        public IntPtr Pointer { get; private set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public float Ratio { get; set; }

        public Image()
        { }

        public Image(IntPtr texture, int width, int height)
        {
            if (texture.ToInt32() == 0) throw new ArgumentOutOfRangeException(nameof(texture));

            Pointer = texture;

            Width = width;
            Height = height;

            Ratio = (float)height / (float)width;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Draw(Vector2 size, Vector4 tintColor, Vector4 borderColor)
        {
            ImGui.Image(Pointer, size, Vector2.Zero, Vector2.One, tintColor, borderColor);
        }
    }
}