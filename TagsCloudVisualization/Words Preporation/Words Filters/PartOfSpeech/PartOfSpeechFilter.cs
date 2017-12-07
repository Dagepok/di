using System.Collections.Generic;
using System.IO;
using System.Linq;
using NHunspell;
using SharpNL.POSTag;

namespace TagsCloudVisualization.Words_Preporation.Words_Filters.PartOfSpeech
{
    public class PartOfSpeechFilter : IWordFilter
    {
        private readonly PartOfSpeechGetter posGetter;
        private readonly IEnumerable<PartsOfSpeech> suitablePos;

        public PartOfSpeechFilter(Settings.Settings settings, PartOfSpeechGetter posGetter)
        {
            this.posGetter = posGetter;
            suitablePos = settings.SuitablePos.Equals(PartsOfSpeech.None)
                ? new[] { PartsOfSpeech.Adjective, PartsOfSpeech.Noun, PartsOfSpeech.Verb }
                : new[] { settings.SuitablePos };
        }

        public List<string> GetSuitableWords(IEnumerable<string> words)
        {
            using (var hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                using (var posTagger = GetTagger())
                {
                    var result = new List<string>();
                    foreach (var word in words)
                    {
                        var stem = hunspell.Stem(word).FirstOrDefault() ?? word;
                        var pos = posGetter.GetPartOfSpeech(posTagger.Tag(new[] { word })[0].ToLowerInvariant());
                        if (stem.Length > 3 && suitablePos.Contains(pos))
                            result.Add(stem);
                    }
                    return result;
                }
            }
        }

        private POSTaggerME GetTagger()
        {
            POSModel posModel;
            using (var modelFile = new FileStream("en-pos-maxent.bin", FileMode.Open))
                posModel = new POSModel(modelFile);
            return new POSTaggerME(posModel);
        }
    }
}