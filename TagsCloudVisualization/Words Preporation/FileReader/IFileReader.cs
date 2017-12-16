using System.Collections.Generic;
using ResultOf;

namespace TagsCloudVisualization.Words_Preporation.FileReader
{
    public interface IFileReader
    {
        Result<List<string>> GetWords();
    }
}