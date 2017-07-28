using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    public delegate void RenderingEventHandler(object sender, RenderingEventArgs e);
    public class RenderingEventArgs
    {
        public TimeSpan TotalElapesTime { get; set; }
        public TimeSpan ElapseTime { get; set; }
    }
}
