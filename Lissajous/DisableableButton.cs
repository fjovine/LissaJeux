//-----------------------------------------------------------------------
// <copyright file="DisableableButton.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    /// <summary>
    /// Widget that features a list of fallible controls, i.e. controls that can have incorrect values, that is disabled even by only one control
    /// in error state.
    /// </summary>
    public class DisableableButton : Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisableableButton"/> class.
        /// </summary>
        public DisableableButton()
        {
            this.FallibleControls = new List<IFallible>();
        }

        /// <summary>
        /// Gets or sets the list of fallible controls whose state determines the state of the button.
        /// </summary>
        public List<IFallible> FallibleControls
        {
            get;
            set;
        }

        /// <summary>
        /// Sets a value indicating whether all the registered fallible controls are enabled.
        /// </summary>
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

        /// <summary>
        /// Checks its own state, i.e. asks all the fallible registered controls if they are in error state and if even one 
        /// only control is in error state, then the button is disabled.
        /// </summary>
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
