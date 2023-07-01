//-----------------------------------------------------------------------
// <copyright file="GifParameters.xaml.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using Microsoft.Win32;
    using System.Windows;

    /// <summary>
    /// Interaction logic behind the <c>GifParameters.xaml</c> form
    /// </summary>
    public partial class GifParameters : Window
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="GifParameters"/> class from being created.
        /// </summary>
        private GifParameters()
        {
            this.InitializeComponent();
            this.OK.FallibleControls.Add(this.XSize);
            this.OK.FallibleControls.Add(this.YSize);
            this.OK.FallibleControls.Add(this.Duration);
            this.XSize.AcceptButton = this.OK;
            this.YSize.AcceptButton = this.OK;
            this.Duration.AcceptButton = this.OK;
        }

        /// <summary>
        /// Gets the user-inserted width of the GIF in pixels.
        /// </summary>
        public int PixelWidth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user-inserted height of the GIF in pixels.
        /// </summary>
        public int PixelHeight
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the user-inserted duration of the GIF animation in seconds.
        /// </summary>
        public double GifDuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the Cancel button has been clicked.
        /// </summary>
        public bool IsCancel
        {
            get;
            private set;
        }

        public string OutputPath
        {
            get;
            private set;
        }

        /// <summary>
        /// Opens an instance of the <see cref="GifParameters"/> form and returns a reference to the user entered values.
        /// </summary>
        /// <returns>A reference to the programmed values.</returns>
        public static GifParameters Get()
        {
            GifParameters result = new GifParameters();
            result.ShowDialog();
            return result;
        }

        /// <summary>
        /// Handles the click event on the OK button.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.IsCancel = false;
            this.PixelWidth = (int)this.XSize.Value;
            this.PixelHeight = (int)this.YSize.Value;
            this.GifDuration = this.Duration.Value;
            this.OutputPath = this.PathOfFile.Text;
            this.Close();
        }

        /// <summary>
        /// Handles the click event on the Cancel button.
        /// </summary>
        /// <param name="sender">The parameter is not used.</param>
        /// <param name="e">The parameter is not used.</param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.IsCancel = true;
            this.Close();
        }

        private void PathExplorer_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.SaveFileDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.PathOfFile.Text = dialog.FileName;
            }
        }
    }
}
