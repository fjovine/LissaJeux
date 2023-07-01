// <copyright file="CirclePanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.22</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Widget showing a circle with a rotating radius and horizontal or vertical projection lines.
    /// </summary>
    public class CirclePanel : LissajousHelperPanel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the circle has horizontal projection lines.
        /// </summary>
        public bool IsHorizontal
        {
            get;
            set;
        }

        /// <summary>
        /// Renders the current state of the circle.
        /// </summary>
        /// <param name="dc">Graphic context where to render.</param>
        protected override void OnRender(DrawingContext dc)
        {
            if (this.Lissajous == null)
            {
                return;
            }

            this.ClipToBounds = true;
            if (this.Lissajous.TheGenerator != null)
            {
                double semiWidth = this.ActualWidth / 2;
                double semiHeight = this.ActualHeight / 2;

                Pen thinPen = new Pen(Brushes.DarkGray, 1.0);
                thinPen.StartLineCap = PenLineCap.Round;
                thinPen.EndLineCap = PenLineCap.Round;
                SimpleTransform transform = new SimpleTransform(semiWidth, semiHeight, this.Lissajous.ScaleFactor, this.Lissajous.ScaleFactor);
                var center = transform.Direct(new Point(0, 0));
                var phase = this.IsHorizontal ?
                    this.LissajousPanel.TheGenerator.PhaseY() :
                    this.LissajousPanel.TheGenerator.PhaseX() - (Math.PI / 2);

                var amplitude = this.IsHorizontal ?
                    this.LissajousPanel.TheGenerator.AmplitudeY :
                    this.LissajousPanel.TheGenerator.AmplitudeX;

                var periphery = transform.Direct(
                    new Point(
                        amplitude * Math.Cos(phase),
                        amplitude * Math.Sin(phase)));                

                double radius = this.Distance(center, periphery);

                dc.DrawEllipse(Brushes.Transparent, thinPen, center, radius, radius);
                dc.DrawLine(thinPen, center, periphery);
                if (this.IsHorizontal)
                {
                    dc.DrawLine(thinPen, periphery, new Point(0, periphery.Y));
                }
                else
                {
                    dc.DrawLine(thinPen, periphery, new Point(periphery.X, this.ActualHeight));
                }
            }
        }

        /// <summary>
        /// Measures the distance between the passed point.
        /// </summary>
        /// <param name="a">First point.</param>
        /// <param name="b">Second point.</param>
        /// <returns>The distance between the point.</returns>
        private double Distance(Point a, Point b)
        {
            double dX = a.X - b.X;
            double dY = a.Y - b.Y;

            return Math.Sqrt((dX * dX) + (dY * dY));
        }
    }
}
