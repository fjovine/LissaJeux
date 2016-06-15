//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
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
            this.HeadX.Lissajous = this.Lissa;
            this.TailX.Lissajous = this.Lissa;
            this.HeadY.Lissajous = this.Lissa;
            this.TailY.Lissajous = this.Lissa;
        }

        /// <summary>
        /// Handles the closing event.
        /// </summary>
        /// <param name="e">Arguments of the closing event.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.Lissa.Stop();
        }

        /// <summary>
        /// Handles the click event on the StopButton
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.Lissa.Stop();
        }
    }
}
