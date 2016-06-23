//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for <c>MainWindow.xaml</c>
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// Entry point of the <c>Lissajous</c> App.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.StartStopButton.FallibleControls.Add(this.AmplitudeRatio);
            this.StartStopButton.FallibleControls.Add(this.FrequencyRatio);
            this.StartStopButton.FallibleControls.Add(this.Phase);
            this.AmplitudeRatio.AcceptButton = this.StartStopButton;
            this.FrequencyRatio.AcceptButton = this.StartStopButton;
            this.Phase.AcceptButton = this.StartStopButton;
        }

        /// <summary>
        /// Handles the closing event.
        /// </summary>
        /// <param name="e">Arguments of the closing event.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.LissaSet.Stop();
        }

        /// <summary>
        /// Handles the click event on the StopButton
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.LissaSet.IsRunning)
            {
                this.StartStopButton.Content = "Start";
                this.LissaSet.Stop();
                this.StartStopButton.EnableAll = true;
                this.ToAnimatedGif.IsEnabled = true;
            }
            else
            {
                Generator generator = new Generator(
                    (float)this.AmplitudeRatio.Value,
                    (float)this.FrequencyRatio.Value,
                    (float)(this.Phase.Value * Math.PI / 180.0),
                    0.04f);
                this.LissaSet.SetGenerator(generator);
                this.LissaSetCircle.SetGenerator(generator);

                this.LissaSet.Start();
                this.StartStopButton.EnableAll = false;
                this.ToAnimatedGif.IsEnabled = false;
                this.StartStopButton.Content = "Stop";
            }
        }

        private void ToAnimatedGif_Click(object sender, RoutedEventArgs e)
        {
            //UserControl usertoBeGifAnimatedConto
            Generator generator = new Generator(
                (float)this.AmplitudeRatio.Value,
                (float)this.FrequencyRatio.Value,
                (float)(this.Phase.Value * Math.PI / 180.0),
                0.04f);

            if (this.TabRender.SelectedIndex == 0)
            {
                LissaSet lissaSet = new LissaSet();
                lissaSet.SetGenerator(generator);
                lissaSet.RenderToGif("prova.gif");
            }
            else
            {
                LissaSetCircle lissaSetCircle = new LissaSetCircle();
                lissaSetCircle.SetGenerator(generator);
                lissaSetCircle.RenderToGif("circle.gif");
            }
        }
    }
}
