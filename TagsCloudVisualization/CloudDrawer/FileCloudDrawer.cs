using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.CloudDrawer
{
    public class FileCloudDrawer : ICloudDrawer
    {
        public FileCloudDrawer(DrawerSettings settings)
        {
            Settings = settings;
        }

        public DrawerSettings Settings { get; }

        public void Draw(List<Tag> tags)
        {
            var bitmap = new Bitmap(Settings.ImageSize.Width, Settings.ImageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            tags.ForEach(tag => graphics.DrawString(tag.Word, tag.Font, tag.Color, tag.Rectangle));
            bitmap.Save(Path.Combine(Settings.Path, Settings.Name + "." + Settings.Format), Settings.Format);
        }
    }
}