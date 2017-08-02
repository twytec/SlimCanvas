using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    /// <summary>
    /// to be added
    /// </summary>
    public class RotationProperty : BasicProperty
    {
        /// <summary>
        /// Angle in degrees 0-360
        /// </summary>
        public RotationProperty(UIElement element) : base(element)
        {
            myValue = 0d;
        }
    }
}
