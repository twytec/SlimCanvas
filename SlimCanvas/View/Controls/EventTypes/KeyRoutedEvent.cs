using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.EventTypes
{
    public delegate void KeyRoutedEventHandler(object sender, KeyRoutedEventArgs e);
    public class KeyRoutedEventArgs : System.EventArgs
    {
        public Keys Key { get; set; }
        public bool Handle { get; set; }
        public uint RepeatCount { get; set; }
        public uint ScanCode { get; set; }
        public string DeviceId { get; set; }
    }

    
}
