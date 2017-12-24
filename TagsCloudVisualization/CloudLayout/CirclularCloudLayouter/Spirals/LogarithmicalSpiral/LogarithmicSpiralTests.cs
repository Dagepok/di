using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals.LogarithmicalSpiral
{
    [TestFixture]
    public class LogarithmicSpiralTests
    {
        [SetUp]
        public void SetUp()
        {
            spiral = new LogarithmicSpiral(new LogarithmicSpiralSettings(new Settings.Settings()));
        }

        private static double CalculateDistanceBetween(PointF point, PointF center)
        {
            return Math.Sqrt(Math.Pow(point.X - center.X, 2) + Math.Pow(point.Y - center.Y, 2));
        }

        private static double DeflectionAngle(double b, double a, double r)
        {
            return 1 / b * Math.Log(r / a);
        }

        private LogarithmicSpiral spiral;

        [Test]
        public void GetNextPoint_FirstPoint_ShouldBeCenter()
        {
            var point = spiral.GetNextPoint();

            point.Should().Be(spiral.Settings.Center);
        }

        [Test]
        public void GetNextPoint_ShouldGetPoints_FromSpiral()
        {
            var pointAngles = new List<double>();
            var pointDistances = new List<double>();

            for (var i = 0; i < 1000; i++)
            {
                pointDistances.Add(CalculateDistanceBetween(spiral.GetNextPoint(), spiral.Settings.Center));
                pointAngles.Add(DeflectionAngle(spiral.Settings.TurnsDistance, spiral.Settings.TurnsRadius,
                    pointDistances[i]));
            }
            for (var i = 1; i < pointAngles.Count; i++)
                pointAngles[i].Should().BeApproximately(spiral.Settings.AngleShift * (i - 1), 1e-3);
        }

        [Test]
        public void GetNextPoint_ShouldNot_GetEqualPoints()
        {
            var points = new List<PointF>();
            for (var i = 0; i < 100; i++)
            {
                var point = spiral.GetNextPoint();
                points.Should().NotContain(point);
                points.Add(point);
            }
        }
    }
}