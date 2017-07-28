using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    public delegate void PointerRoutedEventHandler(object sender, PointerRoutedEventArgs e);
    public class PointerRoutedEventArgs : System.EventArgs
    {
        public int PointerId { get; set; }
        public PointerType PointerType { get; set; }
        public GestureType GestureType { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool Handle { get; set; }

        public Vector2 ToVector2
        {
            get
            {
                return new Vector2(X, Y);
            }
        }

    }

    public enum PointerType
    {
        None,
        Mouse,
        TouchPanel,
        Gamepad,
        Pen
    }

    public enum GestureType
    {
        None,
        Tapped,
        RightTapped,
        SwipeLeft,
        SwipeRight,
        SwipeTop,
        SwipeBottom
    }
}
