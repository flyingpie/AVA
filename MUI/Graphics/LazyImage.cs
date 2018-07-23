using System;
using System.Threading.Tasks;

namespace MUI.Graphics
{
    public class LazyImage : Image
    {
        private Image _image;

        private Task _asc;

        public LazyImage(Image defaultImage, Func<Image> loader)
        {
            _image = defaultImage;

            _asc = Task.Run(() =>
            {
                try
                {
                    _image = loader();

                    Console.WriteLine($"Loaded image");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not load image: {ex.Message}");
                }
            });
        }

        public override IntPtr GetTexture() => _image.GetTexture();
    }
}