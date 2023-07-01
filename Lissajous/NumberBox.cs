//-----------------------------------------------------------------------
// <copyright file="NumberBox.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Fallible <see cref="TextBox"/> that contains a floating point number in simple format (no exponential).
    /// </summary>
    public class NumberBox : TextBox, IFallible
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberBox"/> class.
        /// </summary>
        public NumberBox()
        {
            this.TextChanged += (s, e) =>
            {
                this.Check();
                this.Background = this.IsError ? Brushes.Pink : Brushes.White;
            };
        }

        /// <summary>
        /// Gets or sets the current value shown by the control.
        /// </summary>
        public double Value
        {
            get
            {
                if (!this.IsError)
                {
                    return double.Parse(this.Text);
                }
                else
                {
                    return double.NaN;
                }
            }

            set
            {
                this.Text = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the minimum value the control can contain.
        /// </summary>
        public double MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum value the control can contain.
        /// </summary>
        public double MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the control is in error state.
        /// </summary>
        public bool IsError
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the button that accepts the value contained in this control.
        /// </summary>
        public DisableableButton AcceptButton
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        public bool Enable
        {
            get
            {
                return this.IsEnabled;
            }

            set
            {
                this.IsEnabled = value;
            }
        }

        /// <summary>
        /// Verifies that the content of the textbox can be transformed into a real number and, if not, shows a different background color and
        /// inhibits the accept button.
        /// </summary>
        private void Check()
        {
            double value;

            if (double.TryParse(this.Text, out value))
            {
                if (value < this.MinValue)
                {
                    this.IsError = true;
                }
                else if (value > this.MaxValue)
                {
                    this.IsError = true;
                }
                else
                {
                    this.IsError = false;
                }
            }
            else
            {
                this.IsError = true;
            }

            if (this.AcceptButton != null)
            {
                this.AcceptButton.CheckErrorState();
            }
        }
    }
}
