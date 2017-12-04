using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITagCreator
    {
        List<Tag> GetTags(Dictionary<string, int> wordFrequency);
    }
}