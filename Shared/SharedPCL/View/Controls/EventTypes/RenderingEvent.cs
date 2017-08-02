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
    public delegate void RenderingEventHandler(object sender, RenderingEventArgs e);

    /// <summary>
    /// to be added
    /// </summary>
    public class RenderingEventArgs
    {
        /// <summary>
        /// to be added
        /// </summary>
        public TimeSpan TotalElapesTime { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public TimeSpan ElapseTime { get; set; }
    }
}
