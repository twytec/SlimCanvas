using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    /// <summary>
    /// Create new size
    /// </summary>
    public class Size
    {
        /// <summary>
        /// Width
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Create new size
        /// </summary>
        public Size()
        {

        }

        /// <summary>
        /// Create new size
        /// </summary>
        /// <param name="size"></param>
        public Size(double size)
        {
            Width = size;
            Height = size;
        }

        /// <summary>
        /// Create new size
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
