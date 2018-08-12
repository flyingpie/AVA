using System.Numerics;

namespace MUI.Graphics
{
    public static class ImageMath
    {
        public static void CalculateScaledImageUV(Vector2 imageSize, Vector2 targetSize, ScaleMode scaleMode, out Vector2 uv0, out Vector2 uv1)
        {
            uv0 = Vector2.Zero;
            uv1 = Vector2.One;

            switch (scaleMode)
            {
                case ScaleMode.Center: CalculateScaledImageUVCenter(imageSize, targetSize, out uv0, out uv1); break;
                case ScaleMode.Fill: CalculateScaledImageUVFill(imageSize, targetSize, out uv0, out uv1); break;
                case ScaleMode.Fit: CalculateScaledImageUVFit(imageSize, targetSize, out uv0, out uv1); break;
            }
        }

        public static void CalculateScaledImageUVCenter(Vector2 imageSize, Vector2 targetSize, out Vector2 uv0, out Vector2 uv1)
        {
            var xUVOffset = (targetSize.X / imageSize.X) * .5f;
            var yUVOffset = (targetSize.Y / imageSize.Y) * .5f;

            uv0 = new Vector2(0 - xUVOffset, 0 - yUVOffset);
            uv1 = new Vector2(1 + xUVOffset, 1 + yUVOffset);
        }

        public static void CalculateScaledImageUVFit(Vector2 imageSize, Vector2 targetSize, out Vector2 uv0, out Vector2 uv1)
        {
            var imageRatio = imageSize.X / imageSize.Y;
            var targetRatio = targetSize.X / targetSize.Y;

            var scale = imageRatio > targetRatio ? targetSize.X / imageSize.X : targetSize.Y / imageSize.Y;

            var width = imageSize.X * scale;
            var height = imageSize.Y * scale;

            var xUVOffset = ((targetSize.X - width) * .5f) / imageSize.X;
            var yUVOffset = ((targetSize.Y - height) * .5f) / imageSize.Y;

            uv0 = new Vector2(0 - xUVOffset, 0 - yUVOffset);
            uv1 = new Vector2(1 + xUVOffset, 1 + yUVOffset);
        }

        public static void CalculateScaledImageUVFill(Vector2 imageSize, Vector2 targetSize, out Vector2 uv0, out Vector2 uv1)
        {
            var imageRatio = imageSize.X / imageSize.Y;
            var targetRatio = targetSize.X / targetSize.Y;

            var scale = imageRatio < targetRatio ? targetSize.X / imageSize.X : targetSize.Y / imageSize.Y;

            var width = imageSize.X * scale;
            var height = imageSize.Y * scale;

            var xUVOffset = (width - targetSize.X) * .5f / imageSize.X;
            var yUVOffset = (height - targetSize.Y) * .5f / imageSize.Y;

            uv0 = new Vector2(0 + xUVOffset, 0 + yUVOffset);
            uv1 = new Vector2(1 - xUVOffset, 1 - yUVOffset);
        }
    }
}