using System;

namespace MUI.Graphics
{
    public class Image
    {
        private IntPtr _texture;

        public Image(IntPtr texture)
        {
            _texture = texture;
        }

        public virtual IntPtr GetTexture()
        {
            return _texture;
        }
    }
}