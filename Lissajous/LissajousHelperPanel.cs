// <copyright file="LissajousHelperPanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.22</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Controls;

    /// <summary>
    /// Panel showing a <c>Lissajous</c> curve.
    /// </summary>
    public class LissajousHelperPanel : Canvas
    {
        /// <summary>
        /// Gets or sets the panel where the <c>Lissajous</c> curve is being shown.
        /// </summary>
        public LissajousPanel Lissajous
        {
            get
            {
                return this.LissajousPanel;
            }

            set
            {
                this.LissajousPanel = value;
                if (this.LissajousPanel.TheGenerator != null)
                {
                    this.LissajousPanel.TheGenerator.NewPoint += (s, e) =>
                    {
                        this.Dispatcher.Invoke(() => this.InvalidateVisual());
                    };
                }
            }
        }

        /// <summary>
        /// Gets or sets the local copy of the panel where the curve is shown.
        /// </summary>
        protected LissajousPanel LissajousPanel
        {
            get;
            set;
        }
    }
}
