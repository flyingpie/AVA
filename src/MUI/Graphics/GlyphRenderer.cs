using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

using SysDraw = System.Drawing;

namespace MUI.Graphics
{
	public class GlyphRenderer
	{
		public static SysDraw.Image RenderGlyph(string glyph, FontFamily fontFamily, int fontSizeEm, Color color)
		{
			// Create font
			var font = new Font(fontFamily, fontSizeEm);

			// Measure glyph size
			var measurer = SysDraw.Graphics.FromImage(new Bitmap(1, 1));
			var size = measurer.MeasureString(glyph, font);

			// Create image
			var image = new Bitmap((int)size.Width, (int)size.Height);

			// Draw glyph
			using (var graphics = SysDraw.Graphics.FromImage(image))
			{
				graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
				graphics.PixelOffsetMode = PixelOffsetMode.Half;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.TextContrast = 1;

				graphics.DrawString(glyph, font, new SolidBrush(color), 0, 0);
			}

			return image;
		}

		public static FontFamily LoadFontFamily(string path) => LoadFontFamily(File.ReadAllBytes(path));

		public static FontFamily LoadFontFamily(byte[] fontBytes)
		{
			using (var fonts = new PrivateFontCollection())
			{
				var mem = Marshal.AllocCoTaskMem(fontBytes.Length);

				try
				{
					Marshal.Copy(fontBytes, 0, mem, fontBytes.Length);

					fonts.AddMemoryFont(mem, fontBytes.Length);

					var fontFamily = fonts.Families[0];

					return fontFamily;
				}
				finally
				{
					Marshal.FreeCoTaskMem(mem);
				}
			}
		}
	}
}