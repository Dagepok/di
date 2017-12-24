using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Words_Preporation.FileReader
{
    public class TxtReader : IFileReader
    {
        private TxtReaderSettings Settings { get; }
        public TxtReader(TxtReaderSettings settings) => Settings = settings;

        public Result<List<string>> GetWords() => Result.Of(() => File.ReadAllLines(Settings.SourcePath).ToList());

    }
}