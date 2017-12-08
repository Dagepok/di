using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals;
using TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals.LogarithmicalSpiral;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.CloudLayout.CirclularCloudLayouter
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            var settings = new Settings.Settings();
            var spiral = Substitute.For<ISpiral>();
            spiral.GetNextPoint().Returns(new PointF(0, 0), new PointF(8, 0), new PointF(7, -5), new PointF(0, -10),
                new PointF(-10, -10), new PointF(-15, 0), new PointF(-12, 12), new PointF(0, 15), new PointF(15, 15));
            circularCloud = new CircularCloudLayouter(spiral, settings);
        }

        private CircularCloudLayouter circularCloud;

        [TestCase(0, TestName = "NoElements_AfterCreating")]
        [TestCase(1, TestName = "OneElement_AfterOneAddition")]
        [TestCase(2, TestName = "TwoElements_AfterTwoAdditions")]
        [Timeout(1000)]
        public void CircularCloudLayouter_ShouldHave(int count)
        {
            FillCloudWithRandomRectangles(count);

            circularCloud.Rectangles.Count.Should().Be(count);
        }

        [TestCase(10, 10, TestName = "When positive size")]
        [TestCase(0, 0, TestName = "When zero size")]
        [TestCase(-10, -10, TestName = "When negative size")]
        public void Rectangles_ShouldHave_RightSize(int width, int height)
        {
            circularCloud.PutNextRectangle(new Size(width, height));
            circularCloud.Rectangles.First().Size.Should().Be(new Size(width, height));
        }

        private void FillCloudWithRandomRectangles(int rectangleCount, int seed = 1)
        {
            var rand = new Random(seed);
            for (var i = 0; i < rectangleCount; i++)
            {
                var width = rand.Next(5, 20);
                var heigth = rand.Next(5, 20);
                circularCloud.PutNextRectangle(new Size(width, heigth));
            }
        }

        [Test]
        [Timeout(1000)]
        public void CircularCloudLayouter_FirstRectangle_HaveRightPosition()
        {
            FillCloudWithRandomRectangles(1);

            var rect = circularCloud.Rectangles.First();
            rect.Location.Should().Be(new Point(- rect.Width / 2, - rect.Height / 2));
        }

        [Test]
        public void CircularCloudLayouter_Rectangles_AreTight()
        {
            var settings = new Settings.Settings();
            circularCloud = new CircularCloudLayouter(new LogarithmicSpiral(new LogarithmicSpiralSettings(settings)), settings);

            for (var i = 0; i < 9; i++)
                circularCloud.PutNextRectangle(new Size(20, 20));

            foreach (var rectangle in circularCloud.Rectangles)
            {
                rectangle.X.Should().BeGreaterOrEqualTo(-30 + circularCloud.Center.X);
                rectangle.Y.Should().BeGreaterOrEqualTo(-30 + circularCloud.Center.Y);
                rectangle.X.Should().BeLessOrEqualTo(30 + circularCloud.Center.X);
                rectangle.Y.Should().BeLessOrEqualTo(30 + circularCloud.Center.Y);
            }
        }

        [Test]
        public void Rectangles_ShouldNotIntersect_WhenMoreThanOneRectangle()
        {
            FillCloudWithRandomRectangles(100);
            for (var i = 0; i < circularCloud.Rectangles.Count; i++)
                for (var j = 0; j < circularCloud.Rectangles.Count; j++)
                {
                    if (i == j) continue;
                    circularCloud.Rectangles[i].Should()
                        .Match(x => !((Rectangle)x).IntersectsWith(circularCloud.Rectangles[j]),
                            circularCloud.Rectangles[j].ToTestString());
                }
        }
    }

    public static class TestExtensions
    {
        public static string ToTestString(this Rectangle rectangle)
        {
            return
                $"rectangle IntersectsWith  X={rectangle.X},Y={rectangle.Y},Width={rectangle.Width},Height={rectangle.Height}";
        }
    }
}