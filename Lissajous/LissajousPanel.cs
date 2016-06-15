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
        /// Initializes a new instance of the <see cref="LissajousPanel"/> class.
        /// </summary>
        public LissajousPanel()
        {
            // this.TheGenerator = new Generator(1f, 1f, (float)Math.PI/2, 0.02f); // Circle
            this.TheGenerator = new Generator(4.0f, 1.3f, (float)Math.PI / 2 * 0.14f, 0.02f);
            this.TheGenerator.NewPoint += (s, e) =>
            {
                this.Dispatcher.Invoke(() => { this.InvalidateVisual(); });                
            };
        }

        /// <summary>
        /// Gets the uniform scale (scale x = scale y) to be used to render the curve.
        /// </summary>
        public double ScaleFactor
        {
            get
            {
                double scaleFactorX = this.ActualWidth / this.TheGenerator.AmplitudeX;
                double scaleFactorY = this.ActualHeight / this.TheGenerator.AmplitudeY;
                return Math.Min(scaleFactorX, scaleFactorY) / 2;
            }
        }

        /// <summary>
        /// Gets the generator class that manages the real time generation of the points on the curve.
        /// </summary>
        public Generator TheGenerator
        {
            get;
            private set;
        }

        /// <summary>
        /// Stops the animation thread.
        /// </summary>
        public void Stop()
        {
            if (this.TheGenerator.IsRunning)
            {
                this.TheGenerator.Stop();
            }
            else
            {
                this.TheGenerator.Start();
            }
        }

        /// <summary>
        /// Performs the rendering of the <c>Lissajous</c> curve on this panel.
        /// </summary>
        /// <param name="dc">Graphics context to render on.</param>
        protected override void OnRender(DrawingContext dc)
        {
            //this.ClipToBounds = true;
            const int MaxPens = 5;
            double semiWidth = this.ActualWidth / 2;
            double semiHeight = this.ActualHeight / 2;
            double scaleFactor = this.ScaleFactor;

            dc.PushTransform(new TranslateTransform(semiWidth, semiHeight));
            dc.PushTransform(new ScaleTransform(scaleFactor, scaleFactor));
            var inverse = this.LayoutTransform.Inverse;

            Pen thinPen = new Pen(Brushes.DarkGray, 1.0f / scaleFactor);
            Point pointEast = new Point(-semiWidth, this.TheGenerator.PointNow.Y);
            Point pointWest = new Point(semiWidth, this.TheGenerator.PointNow.Y);
            dc.DrawLine(thinPen, pointEast, pointWest);

            Point pointNorth = new Point(this.TheGenerator.PointNow.X, -semiHeight);
            Point pointSouth = new Point(this.TheGenerator.PointNow.X, semiHeight);
            dc.DrawLine(thinPen, pointNorth, pointSouth);

            Point last = new Point(0, 0);
            Pen[] pens = new Pen[MaxPens + 1];
            for (int j = 0; j < MaxPens; j++)
            {
                //pens[j] = new Pen(Brushes.LightGreen, 0.008 + 0.03 * (MaxPens-j));
                var pen = new Pen(Brushes.LightGreen, 0.04);
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

                Point p = new Point(point.X, point.Y);
                if (i > 0)
                {
                    dc.DrawLine(pens[index], last, p);
                    //dc.DrawLine(pens[0], last, p);
                }

                last = p;
                i++;
            }

            dc.Pop();
            dc.Pop();
        }
    }
}
