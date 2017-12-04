using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class TagSettings
    {

        public int MinFontSize;
        public int MaxFontSize;
        public Brush[] FontColors = {Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Yellow};
        public string FontName;

        public TagSettings(Settings settings)
        {
            MaxFontSize = settings.MaxFontSize;
            MinFontSize = settings.MinFontSize;
            FontName = settings.FontName;
        }

    }
}
