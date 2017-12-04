using System.Drawing;

namespace TagsCloudVisualization.CloudLayout
{
    public interface ICloudLayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}