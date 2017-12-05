using System.Collections.Generic;

namespace TagsCloudVisualization.Words_Preporation.WordConverter
{
    public interface IWordConverter
    {
        List<string> Convert(IEnumerable<string> words);
    }
}