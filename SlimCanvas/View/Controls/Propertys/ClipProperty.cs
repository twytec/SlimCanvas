using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class ClipProperty : BasicProperty
    {
        public ClipProperty(UIElement element) : base(element)
        {
            myValue = new Rect();
        }
    }
}
