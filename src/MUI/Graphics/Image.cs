using ImGuiNET;
using System;
using System.Numerics;

namespace MUI.Graphics
{
    public class Image
    {
        public virtual IntPtr Pointer { get; private set; }

        public virtual int Width { get; private set; }

        public virtual int Height { get; private set; }

        public virtual float Ratio { get; private set; }

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

        public virtual void Draw(Vector2 size, Vector4 tintColor, Vector4 borderColor, Vector4 backgroundColor, ScaleMode scaleMode = ScaleMode.Fit)
        {
            // Save current cursor position
            var cursorPosBefore = ImGui.GetCursorScreenPos();

            // Draw background color
            ImGui.Image(ResourceManager.Instance.WhiteImage.Pointer, size, Vector2.Zero, Vector2.One, backgroundColor, Vector4.Zero);

            // Reset cursor position
            ImGui.SetCursorScreenPos(cursorPosBefore);

            ImageMath.CalculateScaledImageBounds(new Vector2(Width, Height), size, scaleMode, out var cursorScreenPos, out var targetImageSize);

            ImGui.SetCursorScreenPos(cursorPosBefore + cursorScreenPos);
            ImGui.PushClipRect(cursorPosBefore, cursorPosBefore + size, true);

            // Draw image
            DrawImage(tintColor, targetImageSize);

            ImGui.PopClipRect();

            // Reset cursor position
            var cursorPosAfter = ImGui.GetCursorScreenPos();
            ImGui.SetCursorScreenPos(cursorPosBefore);

            // Draw border
            ImGui.Image(ResourceManager.Instance.WhiteImage.Pointer, new Vector2(size.X - 2, size.Y - 2), Vector2.Zero, Vector2.One, new Vector4(0f), borderColor);
        }

        protected virtual void DrawImage(Vector4 tintColor, Vector2 targetImageSize)
        {
            ImGui.Image(Pointer, targetImageSize, Vector2.Zero, Vector2.One, tintColor, Vector4.Zero);
        }
    }
}