using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    public interface IUserInput
    {
        event PointerRoutedEventHandler PointerPressed;
        event PointerRoutedEventHandler PointerMoved;
        event PointerRoutedEventHandler PointerReleased;

        //Only Windows with Mouse
        event PointerRoutedEventHandler RightTapped;

        //event KeyRoutedEventHandler KeyDown;
        //event KeyRoutedEventHandler KeyUp;
    }
}
