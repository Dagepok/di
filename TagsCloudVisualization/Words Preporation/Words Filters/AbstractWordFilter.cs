using System.Collections.Generic;

namespace TagsCloudVisualization.Words_Preporation.Words_Filters
{
    public interface IWordFilter
    {
        List<string> GetSuitableWords(IEnumerable<string> words);
    }
}