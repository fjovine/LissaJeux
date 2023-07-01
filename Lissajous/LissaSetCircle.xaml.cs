//-----------------------------------------------------------------------
// <copyright file="LissaSetCircle.xaml.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for <c>LissaCircle.xaml</c>
    /// </summary>
    public partial class LissaSetCircle : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LissaSetCircle"/> class.
        /// </summary>
        public LissaSetCircle()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the point generator class (<see cref="Generator"/>) and harness all the involved controls.
        /// </summary>
        /// <param name="generator">Generator to be used.</param>
        public void SetGenerator(Generator generator)
        {
            this.LissaOnCircle.TheGenerator = generator;
            this.TopCircle.Lissajous = this.LissaOnCircle;
            this.RightCircle.Lissajous = this.LissaOnCircle;
        }

        /// <summary>
        /// Renders the control to a GIF photogram.
        /// </summary>
        /// <param name="parameters">Parameters of the GIF animation.</param>
        /// <param name="progressManager">Interface to show the progress state to a GUI.</param>
        public void RenderToGif(GifParameters parameters, IProgressManager progressManager)
        {
            this.RenderToGif(this.LissaOnCircle.TheGenerator, parameters, progressManager);
        }
    }
}
