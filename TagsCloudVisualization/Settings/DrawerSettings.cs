using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Settings
{
    public class DrawerSettings
    {
        public Size ImageSize;
        public ImageFormat Format;
        public string Path = "";
        public string Name;

        public DrawerSettings(Settings settings)
        {
            Name = settings.OutFileName;
            Format = settings.OutImageFormat;
            ImageSize = new Size(settings.Width, settings.Height);
        }
    }
}