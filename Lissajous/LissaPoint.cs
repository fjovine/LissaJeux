//-----------------------------------------------------------------------
// <copyright file="LissaPoint.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    /// <summary>
    /// Holds the space and time coordinates of a point to be shown as a <c>Lissajous</c>. curve.
    /// </summary>
    public struct LissaPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LissaPoint"/> struct.
        /// </summary>
        /// <param name="x">Horizontal coordinate of the point.</param>
        /// <param name="y">Vertical coordinate of the point.</param>
        /// <param name="t">Time in seconds the point refers to.</param>
        public LissaPoint(double x, double y, double t)
        {
            this.X = x;
            this.Y = y;
            this.T = t;
        }

        /// <summary>
        /// Gets the horizontal coordinate
        /// </summary>
        public double X
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the vertical coordinate
        /// </summary>
        public double Y
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the time in seconds the point refers to
        /// </summary>
        public double T
        {
            get;
            private set;
        }
    }
}
