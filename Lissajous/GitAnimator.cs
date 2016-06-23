using Gif.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Lissajous
{
    public static class GitAnimator
    {
        public static void RenderToGif(this Control control,  Generator generator, string path, int secondsToPlay)
        {
            System.Windows.Forms.UserControl controlContainer = new System.Windows.Forms.UserControl();

            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.Start(path);
            e.SetDelay(50);
            e.SetRepeat(0);

            controlContainer.Width = 400;
            controlContainer.Height = 400;
            var element = new ElementHost() { Child = control, Dock = System.Windows.Forms.DockStyle.Fill };
            controlContainer.Controls.Add(element);
            IntPtr handle = controlContainer.Handle;
            while (generator.CurrentTime < secondsToPlay)
            {

                RenderTargetBitmap bmp = new RenderTargetBitmap(controlContainer.Width, controlContainer.Height, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(control);

                MemoryStream stream = new MemoryStream();
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(stream);

                e.AddFrame(new Bitmap(stream));
                generator.PerformOneStep();
                System.Diagnostics.Debug.WriteLine("QUI " + generator.CurrentTime);
                Application.Current.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new Action(() =>
                    {
                        System.Diagnostics.Debug.WriteLine("Start refresh");
                        controlContainer.Refresh();
                        System.Diagnostics.Debug.WriteLine("End refresh");
                    }));
                Thread.Sleep(10);
            }
            e.Finish();
            controlContainer.Dispose();
        }

    }
}
