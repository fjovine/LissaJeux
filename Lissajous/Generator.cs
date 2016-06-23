//-----------------------------------------------------------------------
// <copyright file="Generator.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.11</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;

    /// <summary>
    /// Generates the points for the <c>Lissajous</c> figure.
    /// The points are organized in the <see cref="head"/>array containing the points before the current time and
    /// the <see cref="tail"/> array, containing the points after the current time.
    /// The current point, i.e. the point corresponding to the <see cref="CurrentTime" /> is normally
    /// stored in <c>tail[0]</c>
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Number of points stored in the <see cref="head"/> and in the <see cref="tail"/> arrays.
        /// Future and past is symmetrical.
        /// </summary>
        public const int HeadTailLength = 70;

        /// <summary>
        /// Reference frequency, i.e. the smallest frequency of the composing signals
        /// </summary>
        public const float ReferenceFrequency = 0.6f;

        /// <summary>
        /// Reference pulsation, i.e. the smallest pulsation of the composing signals.
        /// </summary>
        public const float ReferenceOmega = (float)(2 * Math.PI) * ReferenceFrequency;

        /// <summary>
        /// Points before the current time, ac
        /// </summary>
        private LissaPoint[] head = new LissaPoint[HeadTailLength + 1];

        /// <summary>
        /// Thread that generates the points, that are regularly made available through the <see cref="Eve"/>
        /// </summary>
        private Thread runner;

        /// <summary>
        /// Set when the ending of the generation thread is asked.
        /// </summary>
        private bool stop = false;

        /// <summary>
        /// Points after the current time. The current point is <c>tail[0]</c>
        /// </summary>
        private LissaPoint[] tail = new LissaPoint[HeadTailLength];

        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        /// <param name="amplitudeRatio">Ratio between X and Y amplitude.</param>
        /// <param name="frequencyRatio">Ratio between X and Y frequencies.</param>
        /// <param name="initialPhaseShift">Initial phase difference between signal Y and X</param>
        /// <param name="deltaTime">Time between two successive signal samplings.</param>
        public Generator(float amplitudeRatio, float frequencyRatio, float initialPhaseShift, float deltaTime)
        {
            if (amplitudeRatio < 1.0f)
            {
                this.AmplitudeX = 1.0f;
                this.AmplitudeY = 1.0f / amplitudeRatio;
            }
            else
            {
                this.AmplitudeX = amplitudeRatio;
                this.AmplitudeY = 1.0f;
            }

            if (frequencyRatio < 1.0)
            {
                this.OmegaX = ReferenceOmega;
                this.OmegaY = ReferenceOmega / frequencyRatio;
            }
            else
            {
                this.OmegaX = ReferenceOmega * frequencyRatio;
                this.OmegaY = ReferenceOmega;
            }

            this.Phi = initialPhaseShift;
            this.DeltaT = deltaTime;
            this.CurrentTime = 0;

            //// Compute the head
            int i = 0;
            for (float time = this.DeltaTimeHeadTail; time > this.DeltaT / 2; time -= this.DeltaT, i++)
            {
                this.head[i] = this.PointAt(time);
            }

            //// Compute the tail
            i = 0;
            for (float time = 0; time > -this.DeltaTimeHeadTail && i < HeadTailLength; time -= this.DeltaT, i++)
            {
                this.tail[i] = this.PointAt(time);
            }
        }

        /// <summary>
        /// Handler of the <see cref="NewPoint"/> event, issued when a new point is available.
        /// </summary>
        /// <param name="source">Source of the event</param>
        /// <param name="a">Arguments of the event</param>
        public delegate void NewPointEnventHandler(object source, NewPointEventArgs a);

        /// <summary>
        /// Event triggered when a new point is available.
        /// </summary>
        public event NewPointEnventHandler NewPoint;

        /// <summary>
        /// Gets or sets the horizontal amplitude as a percentage (0..1) of the panel horizontal size.
        /// </summary>
        public float AmplitudeX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the vertical amplitude as a percentage (0..1) of the panel vertical size.
        /// </summary>
        public float AmplitudeY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current time expressed in seconds from the start.
        /// </summary>
        public float CurrentTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the duration of the head and the tail arrays (they have the same length), i.e. time 
        /// distance in seconds between the first and the last sample.
        /// </summary>
        public float DeltaTimeHeadTail
        {
            get
            {
                return HeadTailLength * this.DeltaT;
            }
        }

        /// <summary>
        /// Gets the head (points before current time) of the point queue.
        /// </summary>
        public IEnumerable<LissaPoint> Head
        {
            get
            {
                return new List<LissaPoint>(this.head);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the generator thread is running.
        /// </summary>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the point now, at the current time.
        /// </summary>
        public LissaPoint PointNow
        {
            get
            {
                return this.tail[0];
            }
        }

        /// <summary>
        /// Gets the tail (points after current time) of the point queue.
        /// </summary>
        public IEnumerable<LissaPoint> Tail
        {
            get
            {
                return new List<LissaPoint>(this.tail);
            }
        }

        /// <summary>
        /// Gets or sets the interval between one point and the following one expressed in seconds.
        /// </summary>
        protected float DeltaT
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the pulsation on the horizontal axis.
        /// </summary>
        protected float OmegaX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the pulsation on the vertical axis.
        /// </summary>
        protected float OmegaY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the initial phase in radians
        /// </summary>
        protected float Phi
        {
            get;
            set;
        }

        /// <summary>
        /// Starts the generation thread.
        /// </summary>
        public void Start()
        {
            this.runner = new Thread(
                () =>
                {
                    while (!stop)
                    {
                        this.IsRunning = true;
                        this.PerformOneStep();
                        Thread.Sleep((int)(this.DeltaT * 1000));
                    }

                    this.IsRunning = false;
                });

            this.runner.Start();
        }

        public void PerformOneStep()
        {
            this.CurrentTime += DeltaT;
            this.AddPointNow();
            if (this.NewPoint != null)
            {
                NewPoint(this, new NewPointEventArgs(this.PointNow));
            }
        }

        /// <summary>
        /// Requests the generator thread to stop.
        /// </summary>
        public void Stop()
        {
            this.stop = true;
        }

        /// <summary>
        /// Waits for the generation thread to end.
        /// </summary>
        public void WaitForEnd()
        {
            this.runner.Join();
        }

        /// <summary>
        /// Gets the point at the passed time.
        /// </summary>
        /// <param name="time">Time expressed in second from the generation start.</param>
        /// <returns>The point on the <c>Lissajous</c> curve at the passed time.</returns>
        protected LissaPoint PointAt(double time)
        {
            //// System.Diagnostics.Debug.WriteLine("Time :" + time);
            double x = this.AmplitudeX * Math.Sin(this.PhaseX(time));
            double y = this.AmplitudeY * Math.Sin(this.PhaseY(time));
            return new LissaPoint(x, y, time);
        }

        public double PhaseX(double time)
        {
            return this.OmegaX * time + this.Phi;
        }

        public double PhaseX()
        {
            return this.PhaseX(this.CurrentTime);
        }

        public double PhaseY(double time)
        {
            return this.OmegaY * time;
        }

        public double PhaseY()
        {
            return this.PhaseY(this.CurrentTime);
        }

        /// <summary>
        /// Adds the current point in the head and tail queues.
        /// </summary>
        private void AddPointNow()
        {
            Array.Copy(this.tail, 0, this.tail, 1, HeadTailLength - 1);
            this.tail[0] = this.head[HeadTailLength - 1];
            Array.Copy(this.head, 0, this.head, 1, HeadTailLength);
            this.head[0] = this.PointAt(this.CurrentTime + this.DeltaTimeHeadTail);
        }
    }
}
