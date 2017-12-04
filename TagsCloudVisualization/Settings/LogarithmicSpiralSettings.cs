using System.Drawing;

namespace TagsCloudVisualization.Settings
{
    public class LogarithmicSpiralSettings
    {
        public double TurnsRadius = 10;
        public double TurnsDistance = 0.015;
        public double AngleShift = 0.17453;
        public PointF Center;

        public LogarithmicSpiralSettings(Settings drawerSettings)
        {
            Center = drawerSettings.Center;
        }
    }
}