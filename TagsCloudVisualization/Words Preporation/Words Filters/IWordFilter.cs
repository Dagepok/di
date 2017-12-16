using System.Collections.Generic;
using ResultOf;

namespace TagsCloudVisualization.Words_Preporation.Words_Filters
{
    public interface IWordFilter
    {
        Result<List<string>> GetSuitableWords(IEnumerable<string> words);
    }
}