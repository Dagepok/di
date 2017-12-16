using System.Collections.Generic;
using System.Linq;
using ResultOf;

namespace TagsCloudVisualization.Words_Preporation.WordConverter
{
    public class ToLowerCaseConverter : IWordConverter
    {
        public Result<List<string>> Convert(IEnumerable<string> words) => 
            Result.Of(() 
                => words.Select(x => x.ToLowerInvariant())
                        .ToList(), "Can't convert to lower case");
    }
}