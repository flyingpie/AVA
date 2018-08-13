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
        public static SysDraw.Image RenderGlyph(string glyph, FontFamily fontFamily, int fontSizeEm, Color color, int rotation = 0, int padding = 0)
        {
            // Create font
            var font = new Font(fontFamily, fontSizeEm);

            // Measure glyph size
            var measurer = SysDraw.Graphics.FromImage(new Bitmap(1, 1));
            var size = measurer.MeasureString(glyph, font);
            size.Width += padding * 2;
            size.Height += padding * 2;

            // Create image
            var image = new Bitmap((int)size.Width, (int)size.Height);

            // Draw glyph
            using (var graphics = SysDraw.Graphics.FromImage(image))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.PixelOffsetMode = PixelOffsetMode.Half;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.TextContrast = 1;

                // Rotate
                var moveX = size.Width / 2f;
                var moveY = size.Height / 2f;

                graphics.TranslateTransform(moveX, moveY);
                graphics.RotateTransform(rotation);
                graphics.TranslateTransform(-moveX, -moveY);

                // Draw
                graphics.DrawString(glyph, font, new SolidBrush(color), padding, padding);
            }

            return image;
        }

        public static SysDraw.Image RenderGlyphSpritesheet(string glyph, FontFamily fontFamily, int fontSizeEm, Color color)
        {
            var horizontalTileCount = 20f;
            var verticalTileCount = 20f;
            var padding = 10;
            var tileCount = horizontalTileCount * verticalTileCount;

            var measure = RenderGlyph(glyph, fontFamily, fontSizeEm, color, 0, padding);
            var tileSize = new Size(measure.Width, measure.Height);

            var bmp = new Bitmap((int)(tileSize.Width * horizontalTileCount), (int)(tileSize.Height * verticalTileCount));
            var gr = SysDraw.Graphics.FromImage(bmp);

            var rotation = 0f;

            for (int y = 0; y < verticalTileCount; y++)
            for (int x = 0; x < verticalTileCount; x++)
            {
                rotation += 360f / tileCount;

                var tile = RenderGlyph(glyph, fontFamily, fontSizeEm, color, (int)rotation, padding);

                var tileX = x * tileSize.Width;
                var tileY = y * tileSize.Height;

                gr.DrawImage(tile, tileX, tileY);
            }

            return bmp;
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