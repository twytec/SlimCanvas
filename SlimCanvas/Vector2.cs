using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    /// <summary>
    /// Create new Vector2
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Create new Vector2
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Get as string
        /// </summary>
        /// <returns>X: {X} Y: {Y}</returns>
        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }
    }
}
