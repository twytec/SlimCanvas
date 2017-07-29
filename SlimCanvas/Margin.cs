using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    /// <summary>
    /// Margin
    /// </summary>
    public struct Margin
    {
        /// <summary>
        /// Left
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Top
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Right
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// Bottom
        /// </summary>
        public double Bottom { get; set; }

        /// <summary>
        /// Create new margin
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public Margin(double left, double top, double right, double bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
