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
    public class ClipProperty : BasicProperty
    {
        /// <summary>
        /// to be added
        /// </summary>
        public ClipProperty(UIElement element) : base(element)
        {
            myValue = new Rect();
        }
    }
}
