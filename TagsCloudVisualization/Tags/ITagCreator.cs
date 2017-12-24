using System.Collections.Generic;

namespace TagsCloudVisualization.Tags
{
    public interface ITagCreator
    {
        Result<List<Tag>> GetTags(Dictionary<string, int> wordFrequency);
    }
}