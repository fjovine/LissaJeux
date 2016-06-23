namespace Lissajous
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class NumberBox : TextBox, IFallible
    {
        public NumberBox()
        {
            this.TextChanged += (s, e) =>
            {
                this.Check();
                this.Background = this.IsError ? Brushes.Pink : Brushes.White;
            };
        }


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

        public double MinValue
        {
            get;
            set;
        }

        public double MaxValue
        {
            get;
            set;
        }

        public bool IsError
        {
            get;
            private set;
        }

        public DisableableButton AcceptButton {
            get;
            set;
        }

        public bool Enable
        {
            get
            {
                return base.IsEnabled;
            }

            set
            {
                base.IsEnabled = value;
            }
        }

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
