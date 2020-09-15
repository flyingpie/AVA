using ImGuiNET;
using ImGuiNET.XNA;
using Microsoft.Xna.Framework.Graphics;
using MUI.Graphics;
using MUI.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;

namespace MUI
{
	public class ResourceManager
	{
		public static ResourceManager Instance { get; private set; }

		public Image DefaultImage { get; private set; }

		public Image LoadingImage { get; private set; }

		public Image WhiteImage { get; private set; }

		private ConcurrentDictionary<string, Image> _loadedImages;
		private TextureLoader _textureLoader;

		private GraphicsDevice _graphicsDevice;
		private ImGuiRenderer _imGuiRenderer;

		private ILog _log;

		public ResourceManager(GraphicsDevice graphicsDevice, ImGuiRenderer imGuiRenderer)
		{
			_log = Log.Get(this);

			_loadedImages = new ConcurrentDictionary<string, Image>();
			_textureLoader = new TextureLoader(graphicsDevice, imGuiRenderer);

			_graphicsDevice = graphicsDevice;
			_imGuiRenderer = imGuiRenderer;

			Instance = this;
		}

		public void Init()
		{
			// TODO: Move this
			DefaultImage = _textureLoader.Load(File.ReadAllBytes("Resources/Images/default-image.png"));
			LoadingImage = new AnimatedImage(_textureLoader.Load(File.ReadAllBytes("Resources/Images/loading-image.png")), 9, 5, 40, Direction.TopToBottom, 5);

			var transp = new System.Drawing.Bitmap(1, 1);
			System.Drawing.Graphics.FromImage(transp).Clear(System.Drawing.Color.White);
			WhiteImage = _textureLoader.Load(transp.ToByteArray());
		}

		public ImFontPtr LoadFont(string path, int pixelSize)
		{
			var font = ImGui.GetIO().Fonts.AddFontFromFileTTF(path, pixelSize);

			_imGuiRenderer.RebuildFontAtlas();

			return font;
		}

		public Image LoadGlyph(string glyph, System.Drawing.FontFamily fontFamily, int fontSizeEm)
		{
			try
			{
				return LoadImage($"glyph_{fontFamily.Name}_{fontSizeEm}_{glyph}", loader =>
				{
					var img = GlyphRenderer.RenderGlyph(glyph, fontFamily, fontSizeEm, System.Drawing.Color.White);

					return loader.Load(img.ToByteArray());
				});
			}
			catch (Exception ex)
			{
				_log.Error($"Could not load glyph: '{ex.Message}'", ex);
				return DefaultImage;
			}
		}

		public Image LoadImageFromUrl(string url)
		{
			try
			{
				return LoadImage(url, loader => new LazyImage(DefaultImage, async () =>
				{
					var response = await new HttpClient().GetAsync(url);
					var data = await response.Content.ReadAsByteArrayAsync();

					return loader.Load(data);
				}));
			}
			catch (Exception ex)
			{
				_log.Error($"Could not load image from url '{url}': '{ex.Message}'.", ex);
				return DefaultImage;
			}
		}

		public bool TryLoadImage(string path, out Image image)
		{
			image = null;

			if (string.IsNullOrWhiteSpace(path)) return false;

			try
			{
				image = LoadImage(path);
				return true;
			}
			catch (Exception ex)
			{
				_log.Error($"Could not load image '{path}': '{ex.Message}'", ex);
				return false;
			}
		}

		// TODO: Add size cap
		public Image LoadImage(string path)
		{
			try
			{
				return LoadImage(path, File.ReadAllBytes(path));
			}
			catch (Exception ex)
			{
				_log.Error($"Could not load image '{path}': '{ex.Message}'", ex);
				return DefaultImage;
			}
		}

		// TODO: Add size cap
		public Image LoadImage(string cacheKey, byte[] data)
		{
			try
			{
				return LoadImage(cacheKey, factory => factory.Load(data));
			}
			catch (Exception ex)
			{
				_log.Error($"Could not load image with cache key '{cacheKey}': '{ex.Message}'", ex);
				return DefaultImage;
			}
		}

		public Image LoadImage(string cacheKey, Func<TextureLoader, Image> factory)
		{
			try
			{
				var image = _loadedImages.GetOrAdd(cacheKey, key => factory(_textureLoader));

				image.Initialize();

				return image;
			}
			catch (Exception ex)
			{
				_log.Error($"Could not load image with cache key '{cacheKey}': '{ex.Message}'", ex);
				return DefaultImage;
			}
		}
	}
}