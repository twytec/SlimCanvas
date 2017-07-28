using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EnumTypes
{
    public enum DashStyle
    {
        Solid,
        Dash,
        Dot,
        DashDotDot,

        /// <summary>
        /// The das pattern is specified by an array of floating-point values. Set to DashPattern property
        /// </summary>
        Custom
    }
}
