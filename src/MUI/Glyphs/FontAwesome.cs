using FontAwesomeCS;
using MUI.Graphics;

namespace MUI.Glyphs
{
	public static class FontAwesome
	{
		private static System.Drawing.FontFamily _brands;
		private static System.Drawing.FontFamily _regular;
		private static System.Drawing.FontFamily _solid;

		static FontAwesome()
		{
			_brands = GlyphRenderer.LoadFontFamily(Deps.FontAwesome.FA.fa_brands_400);
			_regular = GlyphRenderer.LoadFontFamily(Deps.FontAwesome.FA.fa_regular_400);
			_solid = GlyphRenderer.LoadFontFamily(Deps.FontAwesome.FA.fa_solid_900);
		}

		public static Image LoadFontAwesomeIcon(this ResourceManager resourceManager, FAIcon icon, int fontSizeEm)
		{
			var faIcon = icon.GetFAIconAttribute();

			return resourceManager.LoadGlyph(faIcon.Unicode, GetFAFontFamily(faIcon.Style), fontSizeEm);
		}

		private static System.Drawing.FontFamily GetFAFontFamily(FAStyle style)
		{
			switch (style)
			{
				case FAStyle.Brands: return _brands;
				case FAStyle.Solid: return _solid;
				default: return _regular;
			}
		}
	}
}