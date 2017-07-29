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
    public delegate void SizeChangedEventHandler(object sender, SizeChangedEventArgs e);

    /// <summary>
    /// to be added
    /// </summary>
    public class SizeChangedEventArgs
    {
        /// <summary>
        /// to be added
        /// </summary>
        public double NewWidth { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public double NewHeight { get; set; }
    }
}
