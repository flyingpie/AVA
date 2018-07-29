using MUI.Logging;
using System;
using System.Threading.Tasks;

namespace MUI.Graphics
{
    public class LazyImage : Image
    {
        private Image _image;

        private Task _asc;

        private ILog _log;

        public LazyImage(Image defaultImage, Func<Image> loader)
        {
            _image = defaultImage;
            _log = Log.Get(this);

            _asc = Task.Run(() =>
            {
                try
                {
                    _image = loader();

                    _log.Info($"Loaded image");
                }
                catch (Exception ex)
                {
                    _log.Info($"Could not load image: {ex.Message}");
                }
            });
        }

        public override IntPtr GetTexture() => _image.GetTexture();
    }
}