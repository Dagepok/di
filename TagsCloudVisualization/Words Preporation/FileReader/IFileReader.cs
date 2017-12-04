using System.Collections.Generic;

namespace TagsCloudVisualization.Words_Preporation.FileReader
{
    public interface IFileReader
    {
        IEnumerable<string> GetWords();
    }
}