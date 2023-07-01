//-----------------------------------------------------------------------
// <copyright file="IProgressManager.cs" company="hiLab">
// Copyright (c) Francesco Iovine.
// </copyright>
// <author>Francesco Iovine iovinemeccanica@gmail.com</author>
// <creation>2016.06.27</creation>
//-----------------------------------------------------------------------
namespace Lissajous
{
    /// <summary>
    /// Interface implemented by a process to communicate its progress to GUI.
    /// </summary>
    public interface IProgressManager
    {
        /// <summary>
        /// The method to be called to send the progress state to the GUI.
        /// </summary>
        /// <param name="total">Total of steps to be done.</param>
        /// <param name="current">Steps done so far.</param>
        /// <returns>True if the process should be interrupted</returns>
        bool Where(double total, double current);
    }
}
