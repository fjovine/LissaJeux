//-----------------------------------------------------------------------
// <copyright file="IFallible.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    /// <summary>
    /// Interface that must be implemented by a control that can fail, i.e. can be in a wrong state that inhibits closing the Form the control is part of.
    /// </summary>
    public interface IFallible
    {
        /// <summary>
        /// Gets a value indicating whether the control is in error state.
        /// </summary>
        bool IsError
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control should be enabled.
        /// </summary>
        bool Enable
        {
            get;
            set;
        }
    }
}
