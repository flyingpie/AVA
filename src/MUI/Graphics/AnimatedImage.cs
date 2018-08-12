using ImGuiNET;
using System;
using System.Numerics;

namespace MUI.Graphics
{
    public class AnimatedImage : Image
    {
        public override int Width => (int)(_spriteSheet.Width * _tileWidthUv);

        public override int Height => (int)(_spriteSheet.Height * _tileHeightUv);

        public override float Ratio => Height / Width;

        private Image _spriteSheet;

        private int _horizontalTileCount;

        private int _verticalTileCount;
        private int _stride;

        private int _imageCount;

        private float _tileWidthUv;
        private float _tileHeightUv;

        private int _currentImageIndex;
        private float _time;
        private float _maxDelta;

        private Vector2 _uv0;
        private Vector2 _uv1;

        private int _lastFrameNumber;

        public AnimatedImage(Image spriteSheet, int horizontalTileCount, int verticalTileCount, int imageCount, Direction direction, int fps)
        {
            _spriteSheet = spriteSheet ?? throw new ArgumentNullException(nameof(spriteSheet));

            _horizontalTileCount = horizontalTileCount;
            _verticalTileCount = verticalTileCount;

            _stride = direction == Direction.LeftToRight ? horizontalTileCount : verticalTileCount;

            _imageCount = imageCount;

            _tileWidthUv = 1f / _horizontalTileCount;
            _tileHeightUv = 1f / _verticalTileCount;

            _maxDelta = 1f / fps;
        }

        public override void Draw(Vector2 size, Vector4 tintColor, Vector4 borderColor, Vector4 backgroundColor, ScaleMode scaleMode = ScaleMode.Fit)
        {
            UpdateActiveTile();

            base.Draw(size, tintColor, borderColor, backgroundColor, scaleMode);
        }

        protected override void DrawImage(Vector4 tintColor, Vector2 targetImageSize)
        {
            ImGui.Image(_spriteSheet.Pointer, targetImageSize, _uv0, _uv1, tintColor, Vector4.Zero);
        }

        private void UpdateActiveTile()
        {
            if (UIContext.Instance.FrameNumber == _lastFrameNumber) return;

            _lastFrameNumber = UIContext.Instance.FrameNumber;

            _time += ImGui.GetIO().DeltaTime;

            if (_time >= _maxDelta)
            {
                var currentImageX = (int)Math.Floor((float)_currentImageIndex / _verticalTileCount);
                var currentImageY = _currentImageIndex % _verticalTileCount;

                _uv0 = new Vector2(currentImageX * _tileWidthUv, currentImageY * _tileHeightUv);
                _uv1 = new Vector2((currentImageX + 1) * _tileWidthUv, (currentImageY + 1) * _tileHeightUv);

                _time = 0;

                _currentImageIndex++;
                if (_currentImageIndex >= _imageCount) _currentImageIndex = 0;
            }
        }
    }

    public enum Direction
    {
        LeftToRight,
        TopToBottom
    }
}