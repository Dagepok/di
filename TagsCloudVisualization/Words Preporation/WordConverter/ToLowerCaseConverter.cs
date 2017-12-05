using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.Words_Preporation.WordConverter
{
    public class ToLowerCaseConverter : IWordConverter
    {
        public List<string> Convert(IEnumerable<string> words) => words.Select(x => x.ToLowerInvariant()).ToList();
    }
}