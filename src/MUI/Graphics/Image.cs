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
		{

		}

		public Image(IntPtr texture, int width, int height)
		{
			Pointer = texture;

			Width = width;
			Height = height;

			Ratio = (float)height / width;
		}

		public virtual void Initialize()
		{
		}

		public virtual void Draw(Vector2 size, Vector4 tintColor, Vector4 borderColor, Vector4 backgroundColor, Margin padding, ScaleMode scaleMode = ScaleMode.Fit)
		{
			var drawAreaOffset = new Vector2(padding.Left, padding.Right);
			var drawAreaSize = new Vector2(size.X - padding.Left - padding.Right, size.Y - padding.Top - padding.Bottom);

			// Save current cursor position
			var cursorPosBefore = ImGui.GetCursorScreenPos();

			// Draw background color
			ImGui.Image(ResourceManager.Instance.WhiteImage.Pointer, size, Vector2.Zero, Vector2.One, backgroundColor, Vector4.Zero);

			// Reset cursor position
			ImGui.SetCursorScreenPos(cursorPosBefore);

			ImageMath.CalculateScaledImageBounds(new Vector2(Width, Height), drawAreaSize, scaleMode, out var cursorScreenPos, out var targetImageSize);

			ImGui.SetCursorScreenPos(cursorPosBefore + cursorScreenPos + drawAreaOffset);
			ImGui.PushClipRect(cursorPosBefore + drawAreaOffset, cursorPosBefore + drawAreaOffset + drawAreaSize, true);

			// Draw image
			DrawImage(tintColor, targetImageSize);

			ImGui.PopClipRect();

			// Reset cursor position
			var cursorPosAfter = ImGui.GetCursorScreenPos();
			ImGui.SetCursorScreenPos(cursorPosBefore);

			// Draw border
			ImGui.Image(ResourceManager.Instance.WhiteImage.Pointer, new Vector2(size.X - 2, size.Y - 2), Vector2.Zero, Vector2.One, new Vector4(0f), borderColor);
		}

		protected virtual void DrawBackground(Vector2 drawAreaSize, Vector4 backgroundColor)
		{
			// Draw background color
			ImGui.Image(ResourceManager.Instance.WhiteImage.Pointer, drawAreaSize, Vector2.Zero, Vector2.One, backgroundColor, Vector4.Zero);
		}

		protected virtual void DrawImage(Vector4 tintColor, Vector2 targetImageSize)
		{
			ImGui.Image(Pointer, targetImageSize, Vector2.Zero, Vector2.One, tintColor, Vector4.Zero);
		}

		public int CalcWidthIfHeightIs(int height) => (int)(Width * (float)Height / height);

		public int CalcHeightIfWidthIs(int width) => (int)(Height * (float)Width / width);
	}
}