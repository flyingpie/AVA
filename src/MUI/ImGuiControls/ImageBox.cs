using FontAwesomeCS;
using MUI.Glyphs;
using MUI.Graphics;
using System;
using System.Numerics;

namespace MUI.ImGuiControls
{
	public class ImageBox
	{
		public Image Image { get; set; }

		public Vector4 BackgroundColor { get; set; } = Vector4.Zero;

		public Vector4 Border { get; set; } = Vector4.Zero;

		public Margin Padding { get; set; } = Margin.Zero;

		public ScaleMode ScaleMode { get; set; } = ScaleMode.Fit;

		public Vector2 Size { get; set; } = new Vector2(100, 100);

		public Vector4 Tint { get; set; } = Vector4.One;

		public ImageBox(Image image)
		{
			Image = image;
		}

		public void Draw()
		{
			Image?.Draw(Size, Tint, Border, BackgroundColor, Padding, ScaleMode);
		}

		public static implicit operator ImageBox(System.Drawing.Image image) => ResourceManager.Instance.LoadImage(Guid.NewGuid().ToString(), image.ToByteArray());

		public static implicit operator ImageBox(Image image) => new ImageBox(image);

		public static implicit operator ImageBox(string path) => ResourceManager.Instance.LoadImage(path);

		public static implicit operator ImageBox(FAIcon faIcon) => ResourceManager.Instance.LoadFontAwesomeIcon(faIcon, 256);
	}
}