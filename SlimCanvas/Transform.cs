using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    public struct Transform
    {
        /// <summary>
        /// 0.0 - 1.0
        /// </summary>
        public float Scaling { get; set; }

        /// <summary>
        /// x0 y0 = top left
        /// x0.5 y0.5 = center
        /// x1 y1 = bottom right
        /// </summary>
        public Vector2 RotationCenter { get; set; }

        /// <summary>
        /// 0-360
        /// </summary>
        public int Angel { get; set; }

        public Vector2 Translation { get; set; }
    }
}
