using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Words_Preporation.FileReader
{
    public class TxtReader : IFileReader
    {
        private TxtReaderSettings Settings { get; }
        public TxtReader(TxtReaderSettings settings) => this.Settings = settings;

        public IEnumerable<string> GetWords() => File.ReadAllLines(Settings.SourcePath)
                                                        .Select(x=>x.ToLowerInvariant());
    }
}