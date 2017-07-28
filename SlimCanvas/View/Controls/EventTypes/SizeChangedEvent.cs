using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    public delegate void SizeChangedEventHandler(object sender, SizeChangedEventArgs e);

    public class SizeChangedEventArgs
    {
        public double NewWidth { get; set; }
        public double NewHeight { get; set; }
    }
}
