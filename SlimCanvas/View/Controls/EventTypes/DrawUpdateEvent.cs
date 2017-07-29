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
    public delegate void DrawUpdateEventHandler(object sender, DrawUpdateEventArgs e);

    /// <summary>
    /// to be added
    /// </summary>
    public class DrawUpdateEventArgs
    {
        /// <summary>
        /// to be added
        /// </summary>
        public TimeSpan Time { get; set; }
    }
}
