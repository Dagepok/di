using System.Collections.Generic;
using ResultOf;

namespace TagsCloudVisualization
{
    public interface ITagCreator
    {
        Result<List<Tag>> GetTags(Dictionary<string, int> wordFrequency);
    }
}