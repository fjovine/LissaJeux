//-----------------------------------------------------------------------
// <copyright file="LissajousPanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Panel showing in real time the <c>Lissajous</c> figure.
    /// </summary>
    public class LissajousPanel : Canvas
    {
        /// <summary>
        /// Local copy of the generator.
        /// </summary>
        private Generator theGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LissajousPanel"/> class.
        /// </summary>
        public LissajousPanel()
        {
        }

        /// <summary>
        /// Gets the uniform scale (scale x = scale y) to be used to render the curve.
        /// </summary>
        public double ScaleFactor
        {
            get
            {
                if (this.TheGenerator == null)
                {
                    return 1.0;
                }
                else
                {
                    double scaleFactorX = this.ActualWidth / this.TheGenerator.AmplitudeX;
                    double scaleFactorY = this.ActualHeight / this.TheGenerator.AmplitudeY;
                    return Math.Min(scaleFactorX, scaleFactorY) / 2;
                }
            }
        }

        /// <summary>
        /// Gets or sets the generator class that manages the real time generation of the points on the curve.
        /// </summary>
        public Generator TheGenerator
        {
            get
            {
                return this.theGenerator;
            }

            set
            {
                this.theGenerator = value;
                this.theGenerator.NewPoint += (s, e) =>
                {
                    this.Dispatcher.Invoke(() => { this.InvalidateVisual(); });
                };
            }
        }

        /// <summary>
        /// Performs the rendering of the <c>Lissajous</c> curve on this panel.
        /// </summary>
        /// <param name="dc">Graphics context to render on.</param>
        protected override void OnRender(DrawingContext dc)
        {
            const int MaxPens = 5;
            double semiWidth = this.ActualWidth / 2;
            double semiHeight = this.ActualHeight / 2;
            double scaleFactor = this.ScaleFactor;

            if (this.TheGenerator == null)
            {
                return;
            }

            SimpleTransform transform = new SimpleTransform(semiWidth, semiHeight, scaleFactor, scaleFactor);
            Pen thinPen = new Pen(Brushes.DarkGray, 1.0f);
            double y = transform.DirectY(this.TheGenerator.PointNow.Y);
            Point pointEast = new Point(0, y);
            Point pointWest = new Point(this.ActualWidth, y);
            dc.DrawLine(thinPen, pointEast, pointWest);

            double x = transform.DirectX(this.TheGenerator.PointNow.X);
            Point pointNorth = new Point(x, 0);
            Point pointSouth = new Point(x, this.ActualHeight);
            dc.DrawLine(thinPen, pointNorth, pointSouth);

            Point last = new Point(0, 0);
            Pen[] pens = new Pen[MaxPens + 1];
            for (int j = 0; j < MaxPens; j++)
            {
                var pen = new Pen(Brushes.LightGreen, MaxPens - j);
                pen.StartLineCap = PenLineCap.Round;
                pen.EndLineCap = PenLineCap.Round;
                pens[j] = pen;
            }

            int i = 0;
            foreach (var point in this.TheGenerator.Tail)
            {
                int index = i / 10;
                if (index >= MaxPens)
                {
                    index = MaxPens;
                }

                Point p = transform.Direct(new Point(point.X, point.Y));
                if (i > 0)
                {
                    dc.DrawLine(pens[index], last, p);
                }

                last = p;
                i++;
            }
        }
    }
}
