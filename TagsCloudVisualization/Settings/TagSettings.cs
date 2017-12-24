using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class TagSettings
    {
        public int BottomBorder;
        public Brush[] FontColors = {Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Yellow};
        public string FontName;
        public int MaxFontSize;
        public int MinFontSize;
        public int RightBorder;

        public TagSettings(Settings settings)
        {
            MaxFontSize = settings.MaxFontSize;
            MinFontSize = settings.MinFontSize;
            FontName = settings.FontName;
            RightBorder = settings.Width;
            BottomBorder = settings.Height;
        }

        public int LeftBorder => 0;
        public int TopBorder => 0;
    }
}