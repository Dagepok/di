using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace TagsCloudVisualization.Words_Preporation.Words_Checker
{
    public class PartOfSpeechChecker : AbstractWordChecker
    {
        public PartOfSpeechChecker(Settings.Settings settings) : base(settings)
        {
        }

        public override List<string> GetSuitableWords(IEnumerable<string> words)
        {
            using (var hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                return words.Where(x => x.Length > 3)
                    .Select(x => hunspell.Stem(x)
                                     .FirstOrDefault() ?? x).ToList();
            }
        }
    }
}