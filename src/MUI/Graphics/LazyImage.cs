using MUI.Logging;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace MUI.Graphics
{
	public class LazyImage : Image
	{
		private Image _image;
		private Func<Task<Image>> _loader;

		private Task _asc;
		private bool _isLoaded;

		private ILog _log;

		public LazyImage(Image defaultImage, Func<Image> loader) : this(defaultImage, () => Task.FromResult(loader()))
		{
		}

		public LazyImage(Image defaultImage, Func<Task<Image>> loader)
		{
			_image = defaultImage;
			_loader = loader;

			_log = Log.Get(this);
		}

		public override void Initialize()
		{
			if (_asc == null && !_isLoaded)
			{
				_asc = Task.Run(async () =>
				{
					try
					{
						_image = await _loader();

						_isLoaded = true;

						_log.Info($"Loaded image");
					}
					catch (Exception ex)
					{
						_log.Info($"Could not load image: {ex.Message}");
					}

					_asc = null;
				});
			}
		}

		public override void Draw(Vector2 size, Vector4 tintColor, Vector4 borderColor, Vector4 backgroundColor, Margin padding, ScaleMode scaleMode = ScaleMode.Fit)
		{
			_image.Draw(size, tintColor, borderColor, backgroundColor, padding, scaleMode);
		}
	}
}