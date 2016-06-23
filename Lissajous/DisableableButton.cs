namespace Lissajous
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    public class DisableableButton : Button
    {
        public DisableableButton()
        {
            this.FallibleControls = new List<IFallible>();
        }

        public List<IFallible> FallibleControls
        {
            get;
            set;
        }

        public bool EnableAll
        {
            set
            {
                foreach (var fallible in this.FallibleControls)
                {
                    fallible.Enable = value;
                }
            }
        }

        public void CheckErrorState()
        {
            bool error = false;

            foreach (var fallible in this.FallibleControls)
            {
                if (fallible.IsError)
                {
                    error = true;
                    break;
                }
            }

            this.IsEnabled = !error;
        }
    }
}
