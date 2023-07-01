//-----------------------------------------------------------------------
// <copyright file="LissaSet.xaml.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for <c>LissaSet.xaml</c>
    /// This user control contains the <c>Lissajous</c> curve with around the sinusoids creating it.
    /// </summary>
    public partial class LissaSet : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LissaSet"/> class.
        /// </summary>
        public LissaSet()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets a value indicating whether the control is running the simulation.
        /// </summary>
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

        /// <summary>
        /// Renders the control to a GIF photogram.
        /// </summary>
        /// <param name="parameters">Parameters of the GIF animation.</param>
        /// <param name="progressManager">Interface to show the progress state to a GUI.</param>
        public void RenderToGif(GifParameters parameters, IProgressManager progressManager)
        {
            this.RenderToGif(this.Lissa.TheGenerator, parameters, progressManager);
        }

        /// <summary>
        /// Sets the point generator class (<see cref="Generator"/>) and harness all the involved controls.
        /// </summary>
        /// <param name="generator">Generator to be used.</param>
        public void SetGenerator(Generator generator)
        {
            this.Lissa.TheGenerator = generator;
            this.HeadX.Lissajous = this.Lissa;
            this.TailX.Lissajous = this.Lissa;
            this.HeadY.Lissajous = this.Lissa;
            this.TailY.Lissajous = this.Lissa;
        }

        /// <summary>
        ///  Starts the simulation.
        /// </summary>
        public void Start()
        {
            this.Lissa.TheGenerator.Start();
        }

        /// <summary>
        /// Asks for the simulation to stop.
        /// </summary>
        public void Stop()
        {
            if (this.Lissa.TheGenerator != null)
            {
                this.Lissa.TheGenerator.Stop();
            }
        }
    }
}
