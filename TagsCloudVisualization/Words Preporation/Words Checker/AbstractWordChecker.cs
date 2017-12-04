using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Words_Preporation.Words_Checker
{
    public abstract class AbstractWordChecker
    {
        protected AbstractWordChecker(Settings.Settings settings) => Settings = settings;

        private Settings.Settings Settings { get; }

        public abstract List<string> GetSuitableWords(IEnumerable<string> words);

        public Dictionary<string, int> GetWordsFrequency(List<string> words)
        {
            var wordsByCount = new Dictionary<string, int>();
            foreach (var word in words)
                if (wordsByCount.ContainsKey(word))
                    wordsByCount[word]++;
                else wordsByCount.Add(word, 1);
            return wordsByCount.OrderByDescending(x=>x.Value).Take(Settings.WordCount)
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}