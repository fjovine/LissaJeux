//-----------------------------------------------------------------------
// <copyright file="SimpleTransform.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows;

    /// <summary>
    /// Implements a simple linear mapping to scale and de-scale points on the graphic plane.
    /// </summary>
    public class SimpleTransform
    {
        /// <summary>
        /// Local copy of the X-offset
        /// </summary>
        private double translateX;

        /// <summary>
        /// Local copy of the Y-offset
        /// </summary>
        private double translateY;

        /// <summary>
        /// Local copy of the scale on the X-axis
        /// </summary>
        private double scaleX;

        /// <summary>
        /// Local copy of the scale on the Y-axis
        /// </summary>
        private double scaleY;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleTransform"/> class.
        /// </summary>
        /// <param name="translateX">Offset of the linear transform on the X-axis</param>
        /// <param name="translateY">Offset of the linear transform on the Y-axis</param>
        /// <param name="scaleX">Scale factor on the X-axis</param>
        /// <param name="scaleY">Scale factor on the Y-axis</param>
        public SimpleTransform(double translateX, double translateY, double scaleX, double scaleY)
        {
            this.translateX = translateX;
            this.translateY = translateY;
            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        /// <summary>
        /// Performs a direct mapping of the passed point
        /// </summary>
        /// <param name="pt">Point to be rescaled.</param>
        /// <returns>The point rescaled.</returns>
        public Point Direct(Point pt)
        {
            double x = this.DirectX(pt.X);
            double y = this.DirectY(pt.Y);
            return new Point(x, y);
        }

        /// <summary>
        /// Performs a direct mapping of the X-coordinate
        /// </summary>
        /// <param name="x">value to map on the X-axis</param>
        /// <returns>Rescaled value</returns>
        public double DirectX(double x)
        {
            return (x * this.scaleX) + this.translateX;
        }

        /// <summary>
        /// Performs a direct mapping of the Y-coordinate
        /// </summary>
        /// <param name="y">value to map on the Y-axis</param>
        /// <returns>Rescaled value</returns>
        public double DirectY(double y)
        {
            return (y * this.scaleY) + this.translateY;
        }
    }
}
