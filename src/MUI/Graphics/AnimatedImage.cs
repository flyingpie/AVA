using ImGuiNET;
using System;
using System.Numerics;

namespace MUI.Graphics
{
    public class AnimatedImage : Image
    {
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

        public override void Draw(Vector2 size, Vector4 tintColor, Vector4 borderColor)
        {
            UpdateActiveTile();

            ImGui.Image(_spriteSheet.Pointer, size, _uv0, _uv1, tintColor, borderColor);
        }

        private void UpdateActiveTile()
        {
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