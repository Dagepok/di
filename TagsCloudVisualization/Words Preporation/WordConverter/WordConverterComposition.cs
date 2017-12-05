using System;
using System.Collections.Generic;
using System.Linq;
using SharpNL.Extensions;

namespace TagsCloudVisualization.Words_Preporation.WordConverter
{
    public class WordConverterComposition
    {
        private readonly IEnumerable<IWordConverter> converters;

        public WordConverterComposition(params IWordConverter[] converters)
            => this.converters = converters;

        public List<string> ConvertByAll(IEnumerable<string> words)
        {
            converters.ForEach((x, i) => words = x.Convert(words));
            return words.ToList();
        }
    }
}