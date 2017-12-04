using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.CloudLayout.CirclularCloudLayouter.Spirals.LogarithmicalSpiral
{
    public class LogarithmicSpiral : ISpiral
    {
        private readonly IEnumerator<PointF> enumerator;


        public LogarithmicSpiral(LogarithmicSpiralSettings settings)
        {
            Settings = settings;
            var possiblePoints = PossiblePoints();
            enumerator = possiblePoints.GetEnumerator();
        }

        internal LogarithmicSpiralSettings Settings { get; }

        public PointF GetNextPoint()
        {
            var point = enumerator.Current;
            enumerator.MoveNext();
            return new PointF(point.X + Settings.Center.X, point.Y + Settings.Center.Y);
        }

        private IEnumerable<PointF> PossiblePoints()
        {
            for (var angle = 0.0;; angle += Settings.AngleShift)
                yield return ConvertToCartesianCoordinates(angle);
            // ReSharper disable once IteratorNeverReturns
        }

        private double GetRadiusByAngle(double angle)
        {
            return Settings.TurnsRadius * Math.Exp(angle * Settings.TurnsDistance);
        }

        private PointF ConvertToCartesianCoordinates(double angle)
        {
            var x = GetRadiusByAngle(angle) * Math.Cos(angle);
            var y = GetRadiusByAngle(angle) * Math.Sin(angle);
            return new PointF((float) x, (float) y);
        }
    }
}