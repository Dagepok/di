using System;
using CommandLine;
using TagsCloudVisualization.Words_Preporation.Words_Filters.PartOfSpeech;

namespace TagsCloudVisualization.IO
{
    public class ConsoleIo : IIoController
    {
        public Settings.Settings Settings { get; }

        public ConsoleIo(string[] args)
        {
            var options = new Options();
            Settings = !Parser.Default.ParseArguments(args, options)
                ? new Settings.Settings()
                : new Settings.Settings(options, new PartOfSpeechGetter());
        }


        public void Output(string value) => Console.WriteLine(value);

        public string Input() => Console.ReadLine();
    }
}