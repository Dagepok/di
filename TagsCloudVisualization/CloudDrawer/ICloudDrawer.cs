using System.Collections.Generic;

namespace TagsCloudVisualization.CloudDrawer
{
    public interface ICloudDrawer
    {
        void Draw(List<Tag> tags);
    }
}