using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    public delegate void DrawUpdateEventHandler(object sender, DrawUpdateEventArgs e);
    public class DrawUpdateEventArgs
    {
        public TimeSpan Time { get; set; }
    }
}
