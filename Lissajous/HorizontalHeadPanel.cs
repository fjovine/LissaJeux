﻿//-----------------------------------------------------------------------
// <copyright file="HorizontalHeadPanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.13</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Media;

    /// <summary>
    /// WPF panel that shows the head (points before current time) of the vertical component of the <c>Lissajous</c> curve as a horizontally rightwards moving sinusoid.
    /// </summary>
    public class HorizontalHeadPanel : SinusPanel
    {
        /// <summary>
        /// Renders the curve
        /// </summary>
        /// <param name="dc">Graphics contest to be used.</param>
        protected override void OnRender(DrawingContext dc)
        {
            if (this.Lissajous == null)
            {
                return;
            }

            if (this.Lissajous.TheGenerator != null)
            {
                this.RenderHorizontal(dc, this.Lissajous.TheGenerator.Head);
            }
        }
    }
}
