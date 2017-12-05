using System.Collections.Generic;

namespace TagsCloudVisualization.Words_Preporation.Words_Filters.PartOfSpeech
{
    public class PartOfSpeechGetter
    {
        private static readonly Dictionary<string, PartsOfSpeech> PartOfSpeech =
            new Dictionary<string, PartsOfSpeech>
            {
                {"nn", PartsOfSpeech.Noun},
                {"nnp", PartsOfSpeech.Noun},
                {"noun", PartsOfSpeech.Noun},
                {"vb", PartsOfSpeech.Verb},
                {"verb", PartsOfSpeech.Verb},
                {"jj", PartsOfSpeech.Adjective},
                {"rb", PartsOfSpeech.Adjective},
                {"adj", PartsOfSpeech.Adjective},
                {"adjective", PartsOfSpeech.Adjective}
            };

        public static PartsOfSpeech GetPartOfSpeech(string alias)
        {
            return alias == null || !PartOfSpeech.ContainsKey(alias.ToLowerInvariant())
                ? PartsOfSpeech.None
                : PartOfSpeech[alias.ToLowerInvariant()];
        }
    }
}