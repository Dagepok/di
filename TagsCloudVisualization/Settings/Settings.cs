using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Words_Preporation.Words_Filters.PartOfSpeech;

namespace TagsCloudVisualization.Settings
{
    public class Settings
    {
        public string FontName = "Comic Sans MS";
        public int Height = 600;
        public int MaxFontSize = 40;
        public int MinFontSize = 20;
        public string OutFileName = "cloud";
        public ImageFormat OutImageFormat = ImageFormat.Png;
        public string SourcePath = "words.txt";
        public int Width = 600;
        public int WordCount = 100;

        public Settings() 
            => Center = new Point(Width / 2, Height / 2);

        public Settings(Options opt, PartOfSpeechGetter posGetter)
        {
            SuitablePos = posGetter.GetPartOfSpeech(opt.SuitablePos);
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

        public PartsOfSpeech SuitablePos { get; set; }
        public Point Center { get; set; }
    }
}