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
    public delegate void SuspendingEventHandler(object sender, SuspendingEventArgs e);

    /// <summary>
    /// to be added
    /// </summary>
    public class SuspendingEventArgs
    {
        /// <summary>
        /// to be added
        /// </summary>
        public DateTimeOffset Deadline { get; set; }
    }
}
