//-----------------------------------------------------------------------
// <copyright file="VerticalHeadPanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Media;

    /// <summary>
    /// WPF panel that shows the head (points before current time) of the horizontal component of the <c>Lissajous</c> curve as a vertically downwards moving sinusoid.
    /// </summary>
    public class VerticalHeadPanel : SinusPanel
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
                this.RenderVertical(dc, this.Lissajous.TheGenerator.Head);
            }
        }
    }
}
