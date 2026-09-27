using System;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Models
{
    public class LissajousModel
    {
        public double Ax { get; set; }
        public double Ay { get; set; }
        public double Fx { get; set; }
        public double Fy { get; set; }
        public double PhiXDeg { get; set; }
        public double PhiYDeg { get; set; }
        public double TimeStep { get; set; }

        public PointCollection CalculatePoints(double canvasWidth, double canvasHeight)
        {
            PointCollection points = new PointCollection();

         
            if (Ax <= 0 || Ay <= 0 || Fx <= 0 || Fy <= 0 || TimeStep <= 0)
                return points;

            double minFrequency = Math.Min(Fx, Fy);
            double maxPeriod = 1.0 / minFrequency;
            double totalTime = maxPeriod * 4; 

            double phiXRad = PhiXDeg * Math.PI / 180.0;
            double phiYRad = PhiYDeg * Math.PI / 180.0;

            double width = canvasWidth > 0 ? canvasWidth : 550;
            double height = canvasHeight > 0 ? canvasHeight : 550;

            double centerX = width / 2.0;
            double centerY = height / 2.0;

            double maxAx = Ax;
            double maxAy = Ay;


            double baseScale = Math.Min(width, height) / 2.5;
            double scaleX = baseScale / maxAx;
            double scaleY = baseScale / maxAy;
            double uniformScale = Math.Min(scaleX, scaleY);

            for (double t = 0; t <= totalTime; t += TimeStep)
            {
                double x = Ax * Math.Sin(2 * Math.PI * Fx * t + phiXRad);
                double y = Ay * Math.Sin(2 * Math.PI * Fy * t + phiYRad);

                double screenX = centerX + x * uniformScale;
                double screenY = centerY - y * uniformScale;

                points.Add(new Point(screenX, screenY));
            }

            return points;
        }
    }
}