//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using FluentAssertions;
//using NUnit.Framework;
//using NUnit.Framework.Interfaces;

//namespace TagsCloudVisualization
//{
//    [TestFixture]
//    public class CircularCloudLayouterTests
//    {
       
//        private CircularCloudLayouter circularCloud;

//        [TestCase(0, TestName = "NoElements_AfterCreating")]
//        [TestCase(1, TestName = "OneElement_AfterOneAddition")]
//        [TestCase(2, TestName = "TwoElements_AfterTwoAdditions")]
//        [Timeout(1000)]
//        public void CircularCloudLayouter_ShouldHave(int count)
//        {
//            circularCloud = new CircularCloudLayouter(new LogarithmicSpiral(new LogarithmicSpiralSettings()));

//            FillCloudWithRandomRectangles(count);

//            circularCloud.Rectangles.WordCount.Should().Be(count);
//        }

//        [Test]
//        [Timeout(1000)]
//        public void CircularCloudLayouter_FirstRectangle_HaveRightPosition()
//        {
//            circularCloud = new CircularCloudLayouter(new LogarithmicSpiral(new LogarithmicSpiralSettings()));

//            FillCloudWithRandomRectangles(1);

//            circularCloud.Rectangles.First().Location.Should().Be(new Point(0, 0));
//        }

//        [Test]
//        public void CircularCloudLayouter_Rectangles_AreTight()
//        {
//            circularCloud = new CircularCloudLayouter(new LogarithmicSpiral(new LogarithmicSpiralSettings()));

//            for (var i = 0; i < 9; i++)
//                circularCloud.PutNextRectangle(new Size(20, 20));

//            foreach (var rectangle in circularCloud.Rectangles)
//            {
//                rectangle.X.Should().BeGreaterOrEqualTo(-20);
//                rectangle.Y.Should().BeGreaterOrEqualTo(-20);
//                rectangle.X.Should().BeLessOrEqualTo(40);
//                rectangle.Y.Should().BeLessOrEqualTo(20);
//            }
//        }

//        [Test]
//        public void Rectangles_ShouldNotIntersect_WhenMoreThanOneRectangle()
//        {
//            circularCloud = new CircularCloudLayouter(new LogarithmicSpiral(new LogarithmicSpiralSettings()));

//            FillCloudWithRandomRectangles(100);
//            for (var i = 0; i < circularCloud.Rectangles.WordCount; i++)
//                for (var j = 0; j < circularCloud.Rectangles.WordCount; j++)
//                {
//                    if (i == j) continue;
//                    circularCloud.Rectangles[i].Should()
//                        .Match(x => !((Rectangle)x).IntersectsWith(circularCloud.Rectangles[j]),
//                            circularCloud.Rectangles[j].ToTestString());
//                }
//        }

//        [TestCase(10, 10, TestName = "When positive size")]
//        [TestCase(0, 0, TestName = "When zero size")]
//        [TestCase(-10, -10, TestName = "When negative size")]
//        public void Rectangles_ShouldHave_RightSize(int width, int height)
//        {
//            circularCloud = new CircularCloudLayouter(new LogarithmicSpiral(new LogarithmicSpiralSettings()));
//            circularCloud.PutNextRectangle(new Size(width, height));
//            circularCloud.Rectangles.First().Size.Should().Be(new Size(width, height));
//        }

//        private void FillCloudWithRandomRectangles(int rectangleCount, int seed = 1)
//        {
//            var rand = new Random(seed);
//            for (var i = 0; i < rectangleCount; i++)
//            {
//                var width = rand.Next(5, 20);
//                var heigth = rand.Next(5, 20);
//                circularCloud.PutNextRectangle(new Size(width, heigth));
//            }
//        }
//    }

//    public static class TestExtensions
//    {
//        public static string ToTestString(this Rectangle rectangle)
//        {
//            return
//                $"rectangle IntersectsWith  X={rectangle.X},Y={rectangle.Y},Width={rectangle.Width},Height={rectangle.Height}";
//        }
//    }
//}