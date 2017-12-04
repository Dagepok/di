using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Settings
{
    public class Settings
    {
        public int Width = 600;
        public int Height = 600;
        public int WordCount = 100;
        public string OutFileName = "cloud";
        public ImageFormat OutImageFormat = ImageFormat.Png;
        public string SourcePath = "words.txt";
        public int MaxFontSize = 40;
        public int MinFontSize = 20;

        public string FontName = "Comic Sans MS";
        public Point Center;
        public Settings() => Center = new Point(Width / 2, Height / 2);

        public Settings(Options opt)
        {
            Width = opt.Width;
            Height = opt.Height;
            WordCount = opt.WordCount;
            OutFileName = opt.OutputFile;
            SourcePath = opt.SourceFile;
            Center = new Point(Width / 2, Height / 2);
            FontName = opt.FontName;
            MaxFontSize = opt.FontSize;
            MinFontSize = MaxFontSize / 4;
        }

    }
}