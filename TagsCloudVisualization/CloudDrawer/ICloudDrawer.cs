using System.Collections.Generic;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.CloudDrawer
{
    public interface ICloudDrawer
    {
        void Draw(List<Tag> tags);
    }
}