using System.Drawing;

namespace TagsCloudVisualization
{
    public class Tag
    {
        public Tag(Rectangle rectangle, Brush color, string word, Font font)
        {
            Rectangle = rectangle;
            Color = color;
            Word = word;
            Font = font;
        }

        public Rectangle Rectangle { get; }
        public Brush Color { get; }
        public string Word { get; }
        public Font Font { get; }
    }
}