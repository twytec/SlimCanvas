using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    /// <summary>
    /// Only internal interfache
    /// </summary>
    public interface IUserInput
    {
        /// <summary>
        /// Only internal interfache
        /// </summary>
        event PointerRoutedEventHandler PointerPressed;

        /// <summary>
        /// Only internal interfache
        /// </summary>
        event PointerRoutedEventHandler PointerMoved;

        /// <summary>
        /// Only internal interfache
        /// </summary>
        event PointerRoutedEventHandler PointerReleased;

        //Only Windows with Mouse
        /// <summary>
        /// Only internal interfache
        /// </summary>
        event PointerRoutedEventHandler RightTapped;
    }
}
