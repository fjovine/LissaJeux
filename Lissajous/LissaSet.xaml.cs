using Gif.Components;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Lissajous
{
    /// <summary>
    /// Logica di interazione per LissaSet.xaml
    /// </summary>
    public partial class LissaSet : UserControl
    {
        public LissaSet()
        {
            InitializeComponent();
        }

        public void SetGenerator(Generator generator)
        {
            this.Lissa.TheGenerator = generator;
            this.HeadX.Lissajous = this.Lissa;
            this.TailX.Lissajous = this.Lissa;
            this.HeadY.Lissajous = this.Lissa;
            this.TailY.Lissajous = this.Lissa;
        }

        public bool IsRunning
        {
            get
            {
                if (this.Lissa.TheGenerator == null)
                {
                    return false;
                }

                return this.Lissa.TheGenerator.IsRunning;
            }
        }

        public void Stop()
        {
            this.Lissa.TheGenerator.Stop();
        }

        public void Start()
        {
            this.Lissa.TheGenerator.Start();
        }

        public void RenderToGif(string path)
        {
            this.RenderToGif(this.Lissa.TheGenerator, path, 5);
        }
    }
}
