using System.Numerics;

namespace MUI.Graphics
{
	public static class ImageMath
	{
		public static void CalculateScaledImageBounds(
			Vector2 imageSize,
			Vector2 drawSize,
			ScaleMode scaleMode,

			out Vector2 relativeCursorScreenPos,
			out Vector2 targetImageSize)
		{
			relativeCursorScreenPos = Vector2.Zero;
			targetImageSize = imageSize;

			var imageRatio = imageSize.X / imageSize.Y;
			var targetRatio = drawSize.X / drawSize.Y;

			switch (scaleMode)
			{
				case ScaleMode.Center:
					{
						relativeCursorScreenPos = new Vector2
						(
							drawSize.X * .5f - imageSize.X * .5f,
							drawSize.Y * .5f - imageSize.Y * .5f
						);

						break;
					}

				case ScaleMode.Fill:
					{
						var scale = imageRatio < targetRatio ? drawSize.X / imageSize.X : drawSize.Y / imageSize.Y;

						var w = imageSize.X * scale;
						var h = imageSize.Y * scale;

						targetImageSize = new Vector2(w, h);

						relativeCursorScreenPos = new Vector2
						(
							drawSize.X * .5f - w * .5f,
							drawSize.Y * .5f - h * .5f
						);

						break;
					}

				case ScaleMode.Fit:
					{
						var scale = imageRatio > targetRatio ? drawSize.X / imageSize.X : drawSize.Y / imageSize.Y;

						var w = imageSize.X * scale;
						var h = imageSize.Y * scale;

						targetImageSize = new Vector2(w, h);

						relativeCursorScreenPos = new Vector2
						(
							drawSize.X * .5f - w * .5f,
							drawSize.Y * .5f - h * .5f
						);

						break;
					}

				case ScaleMode.Stretch:
				default:
					{
						relativeCursorScreenPos = Vector2.Zero;
						targetImageSize = drawSize;

						break;
					}
			}
		}
	}
}