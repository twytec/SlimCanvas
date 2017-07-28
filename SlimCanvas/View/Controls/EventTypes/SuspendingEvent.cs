using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    public delegate void SuspendingEventHandler(object sender, SuspendingEventArgs e);
    public class SuspendingEventArgs
    {
        public DateTimeOffset Deadline { get; set; }
    }
}
