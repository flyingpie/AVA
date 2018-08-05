using ImGuiNET;
using System;
using System.Numerics;

namespace MUI.Graphics
{
    public class Image
    {
        public IntPtr Pointer { get; private set; }

        public Image()
        { }

        public Image(IntPtr texture)
        {
            if (texture.ToInt32() == 0) throw new ArgumentOutOfRangeException(nameof(texture));

            Pointer = texture;
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