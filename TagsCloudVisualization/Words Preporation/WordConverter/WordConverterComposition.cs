using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Words_Preporation.WordConverter
{
    public class WordConverterComposition
    {
        private readonly IEnumerable<IWordConverter> converters;

        public WordConverterComposition(params IWordConverter[] converters)
        {
            this.converters = converters;
        }

        public Result<List<string>> Convert(IEnumerable<string> words)
        {
            foreach (var wordConverter in converters)
            {
                var result = wordConverter.Convert(words);
                if (result.IsSuccess)
                    words = result.Value;
                else
                    return result;
            }
            return Result.Ok(words.ToList());
        }
    }
}