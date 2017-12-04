using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals;

namespace TagsCloudVisualization.CloudLayout.CirclularCloudLayouter
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> rectangles;

        public CircularCloudLayouter(ISpiral spiral, Settings.Settings settings)
        {
            Center = Point.Truncate(settings.Center);
            Spiral = spiral;
            rectangles = new List<Rectangle>();
        }

        private ISpiral Spiral { get; }
        public Point Center { get; }
        public IReadOnlyList<Rectangle> Rectangles => rectangles;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangle = GetNextRectangle(rectangleSize);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle GetNextRectangle(Size rectangleSize)
        {
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(Point.Round(Spiral.GetNextPoint()), rectangleSize);
            } while (IsRectangleIntersectsWithOther(rectangle));
            return Rectangles.Count == 0 ? ShiftFirstRect(rectangle) : GetMovedToCenter(rectangle);
        }

        private Rectangle ShiftFirstRect(Rectangle rectangle)
        {
            rectangle.Offset(-rectangle.Width / 2, -rectangle.Height / 2);
            return rectangle;
        }

        private Rectangle GetMovedToCenter(Rectangle rectangle)
        {
            while (true)
            {
                var lastRectangle = rectangle;
                rectangle = TryMoveToCenter(rectangle);
                if (lastRectangle.Equals(rectangle)) break;
            }
            return rectangle;
        }

        private Rectangle TryMoveToCenter(Rectangle rectangle)
        {
            var vectorToCenter = new Point(Center.X - rectangle.X, Center.Y - rectangle.Y);
            if (vectorToCenter.X != 0)
                rectangle = TryMove(rectangle, new Point(Math.Sign(vectorToCenter.X), 0));
            if (vectorToCenter.Y != 0)
                rectangle = TryMove(rectangle, new Point(0, Math.Sign(vectorToCenter.Y)));
            return rectangle;
        }

        private Rectangle TryMove(Rectangle rectangle, Point offset)
        {
            rectangle.Offset(offset);
            if (IsRectangleIntersectsWithOther(rectangle))
                rectangle.Offset(new Point(-offset.X, -offset.Y));
            return rectangle;
        }

        private bool IsRectangleIntersectsWithOther(Rectangle rectangle)
        {
            return rectangles.Any(rectangle.IntersectsWith);
        }
    }
}