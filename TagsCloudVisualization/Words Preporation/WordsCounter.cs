using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Words_Preporation
{
    public class WordsCounter
    {
        public Dictionary<string, int> GetWordsFrequency(List<string> words, int wordsCount)
            => words.GroupBy(word => word)
                .OrderByDescending(x => x.Count())
                .Take(wordsCount)
                .ToDictionary(x => x.Key, x => x.Count());

    }
}