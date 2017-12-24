using System.Collections.Generic;

namespace TagsCloudVisualization.Words_Preporation.WordConverter
{
    public interface IWordConverter
    {
        Result<List<string>> Convert(IEnumerable<string> words);
    }
}