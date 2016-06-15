//-----------------------------------------------------------------------
// <copyright file="SinusPanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.13</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// WPF panel able to show a sinusoid representing the composing signals of the <c>Lissajous curve.</c>
    /// </summary>
    public class SinusPanel : Canvas
    {
        /// <summary>
        /// Local copy of the panel where the curve is shown.
        /// </summary>
        private LissajousPanel lissajousPanel;

        /// <summary>
        /// Gets or sets the panel where the <c>Lissajous</c> curve is being shown.
        /// </summary>
        public LissajousPanel Lissajous
        {
            get
            {
                return this.lissajousPanel;
            }

            set
            {
                this.lissajousPanel = value;
                this.lissajousPanel.TheGenerator.NewPoint += (s, e) =>
                {
                    this.Dispatcher.Invoke(() => this.InvalidateVisual());
                };
            }
        }

        /// <summary>
        /// Renders the horizontally moving composing sinusoid.
        /// </summary>
        /// <param name="dc">Graphics environment where to render.</param>
        /// <param name="toRender">Enumeration of points to render.</param>
        protected void RenderHorizontal(DrawingContext dc, IEnumerable<LissaPoint> toRender)
        {
            //this.ClipToBounds = true;
            if (this.Lissajous == null)
            {
                return;
            }

            double semiHeight = this.ActualHeight / 2;
            double scaleFactorX = this.ActualWidth / this.Lissajous.TheGenerator.DeltaTimeHeadTail;
            dc.PushTransform(new TranslateTransform(0, semiHeight));
            dc.PushTransform(new ScaleTransform(-scaleFactorX, this.Lissajous.ScaleFactor));
            Pen thinPen = new Pen(Brushes.DarkGray, 1.5f / this.Lissajous.ScaleFactor);
            thinPen.StartLineCap = PenLineCap.Round;
            thinPen.EndLineCap = PenLineCap.Round;
            this.Render(thinPen, dc, toRender, (point, startTime) => new Point(point.T - startTime, point.Y));
        }

        /// <summary>
        /// Renders the vertical moving composing sinusoid.
        /// </summary>
        /// <param name="dc">Graphics environment where to render.</param>
        /// <param name="toRender">Enumeration of points to render.</param>
        protected void RenderVertical(DrawingContext dc, IEnumerable<LissaPoint> toRender)
        {
            //this.ClipToBounds = true;
            if (this.Lissajous == null)
            {
                return;
            }

            double semiWidth = this.ActualWidth / 2;
            double scaleFactorY = this.ActualHeight / this.Lissajous.TheGenerator.DeltaTimeHeadTail;
            dc.PushTransform(new TranslateTransform(semiWidth, 0));
            dc.PushTransform(new ScaleTransform(this.Lissajous.ScaleFactor, -scaleFactorY));
            Pen thinPen = new Pen(Brushes.DarkGray, 3.0f / this.Lissajous.ScaleFactor);
            this.Render(thinPen, dc, toRender, (point, startTime) => new Point(point.X, point.T - startTime));
        }

        /// <summary>
        /// Renders the needed sinusoid.
        /// </summary>
        /// <param name="thinPen">Pen to be used.</param>
        /// <param name="dc">Graphics environment where to render.</param>
        /// <param name="toRender">Enumeration of points on the curve to render.</param>
        /// <param name="getPoint">Lambda returning the point to be shown on the composing sinusoid.</param>
        private void Render(Pen thinPen, DrawingContext dc, IEnumerable<LissaPoint> toRender, Func<LissaPoint, double, Point> getPoint)
        {
            bool isFirst = true;
            Point lastPoint = new Point(0, 0);
            double startTime = 0;
            foreach (var point in toRender)
            {
                if (isFirst)
                {
                    startTime = point.T;
                }
                //// Point currentPoint = new Point(point.X, point.t - startTime);
                Point currentPoint = getPoint(point, startTime);
                if (!isFirst)
                {
                    dc.DrawLine(thinPen, lastPoint, currentPoint);
                }

                lastPoint = currentPoint;
                isFirst = false;
            }
        }
    }
}