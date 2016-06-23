// <copyright file="LissajousHelperPanel.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.22</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Controls;

    public class LissajousHelperPanel : Canvas
    {
        /// <summary>
        /// Local copy of the panel where the curve is shown.
        /// </summary>
        protected LissajousPanel lissajousPanel;

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
                if (this.lissajousPanel.TheGenerator != null)
                {
                    this.lissajousPanel.TheGenerator.NewPoint += (s, e) =>
                    {
                        this.Dispatcher.Invoke(() => this.InvalidateVisual());
                    };
                }
            }
        }
    }
}
