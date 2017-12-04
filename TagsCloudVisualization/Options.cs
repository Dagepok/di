using CommandLine;
using CommandLine.Text;

namespace TagsCloudVisualization
{
    public class Options
    {
        [Option('i', "input", DefaultValue = "words.txt", HelpText = "input file")]
        public string SourceFile { get; set; }

        [Option('o', "output", DefaultValue = "cloud", HelpText = "output file name")]
        public string OutputFile { get; set; }

        [Option('w', "width", DefaultValue = 1000, HelpText = "width")]
        public int Width { get; set; }

        [Option('h', "height", DefaultValue = 1000, HelpText = "height")]
        public int Height { get; set; }

        [Option('c', "count", DefaultValue = 100, HelpText = "count of word in cloud")]
        public int WordCount { get; set; }

        [Option('f', "fontName", DefaultValue = "Comic Sans MS", HelpText = "Font Name")]
        public string FontName { get; set; }

        [Option('k', "fontSize", DefaultValue = 60, HelpText = "Font Size")]
        public int FontSize { get; set; }

        [Option('p', "partOfSpeech", HelpText = "Font Size")]
        public string SuitablePos { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }
    }
}