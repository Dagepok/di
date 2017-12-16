using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization.Settings
{
    public class TagSettings
    {
        public int LeftBorder => 0;
        public int RightBorder;
        public int TopBorder => 0;
        public int BottomBorder;
        public int MinFontSize;
        public int MaxFontSize;
        public Brush[] FontColors = { Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Yellow };
        public string FontName;

        public TagSettings(Settings settings)
        {
            MaxFontSize = settings.MaxFontSize;
            MinFontSize = settings.MinFontSize;
            FontName = settings.FontName;
            RightBorder = settings.Width;
            BottomBorder = settings.Height;
        }

    }
}
