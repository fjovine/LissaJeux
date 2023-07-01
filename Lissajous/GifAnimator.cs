//-----------------------------------------------------------------------
// <copyright file="GifAnimator.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
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
    using Gif.Components;

    /// <summary>
    /// Static class managing the animated Gif generation. 
    /// The algorithm renders the WPF control and stores each image on file.
    /// </summary>
    public static class GifAnimator
    {
        /// <summary>
        /// Renders the current state of the <c>Lissajous</c> control.
        /// </summary>
        /// <param name="control">WPF control showing the <c>Lissajous</c> image.</param>
        /// <param name="generator">Generator of the <c>Lissajous</c> positions.</param>
        /// <param name="gifParameters">Parameters to be used for the animation.</param>
        /// <param name="progressManager">Interface to transmit the current process status to GUI.</param>
        public static void RenderToGif(this Control control,  Generator generator, GifParameters gifParameters, IProgressManager progressManager)
        {
            System.Windows.Forms.UserControl controlContainer = new System.Windows.Forms.UserControl();

            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.Start(gifParameters.OutputPath);
            e.SetDelay(50);
            e.SetRepeat(0);

            controlContainer.Width = gifParameters.PixelWidth;
            controlContainer.Height = gifParameters.PixelHeight;
            var element = new ElementHost() { Child = control, Dock = System.Windows.Forms.DockStyle.Fill };
            controlContainer.Controls.Add(element);
            IntPtr handle = controlContainer.Handle;
            while (generator.CurrentTime < (float)gifParameters.GifDuration) 
            {
                if (progressManager.Where((float)gifParameters.GifDuration, generator.CurrentTime))
                {
                    break;
                }

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
