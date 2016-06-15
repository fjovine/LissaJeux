//-----------------------------------------------------------------------
// <copyright file="NewPointEventArgs.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Arguments to the <see cref="NewPoint"/> event.
    /// </summary>
    public class NewPointEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewPointEventArgs"/> class.
        /// </summary>
        /// <param name="currentPoint">The current point that triggered the event.</param>
        public NewPointEventArgs(LissaPoint currentPoint)
        {
            this.CurrentPoint = currentPoint;
        }

        /// <summary>
        /// Gets the current point, i.e. the one that triggered the event.
        /// </summary>
        public LissaPoint CurrentPoint
        {
            get;
            private set;
        }
    }
}