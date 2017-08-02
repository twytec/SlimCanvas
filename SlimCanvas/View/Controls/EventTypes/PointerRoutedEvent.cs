using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    /// <summary>
    /// to be added
    /// </summary>
    public delegate void PointerRoutedEventHandler(object sender, PointerRoutedEventArgs e);

    /// <summary>
    /// to be added
    /// </summary>
    public class PointerRoutedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets a unique identifier for the input pointer
        /// </summary>
        public int PointerId { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public PointerType PointerType { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public GestureType GestureType { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public bool Handle { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public Vector2 ToVector2
        {
            get
            {
                return new Vector2(X, Y);
            }
        }

    }

    /// <summary>
    /// to be added
    /// </summary>
    public enum PointerType
    {
        /// <summary>
        /// to be added
        /// </summary>
        None,

        /// <summary>
        /// to be added
        /// </summary>
        Mouse,

        /// <summary>
        /// to be added
        /// </summary>
        TouchPanel,

        /// <summary>
        /// to be added
        /// </summary>
        Gamepad,

        /// <summary>
        /// to be added
        /// </summary>
        Pen
    }

    /// <summary>
    /// to be added
    /// </summary>
    public enum GestureType
    {
        /// <summary>
        /// to be added
        /// </summary>
        None,

        /// <summary>
        /// to be added
        /// </summary>
        Tapped,

        /// <summary>
        /// to be added
        /// </summary>
        RightTapped,

        /// <summary>
        /// to be added
        /// </summary>
        SwipeLeft,

        /// <summary>
        /// to be added
        /// </summary>
        SwipeRight,

        /// <summary>
        /// to be added
        /// </summary>
        SwipeTop,

        /// <summary>
        /// to be added
        /// </summary>
        SwipeBottom
    }
}
