using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class OriginProperty : BasicProperty
    {
        public OriginProperty(UIElement element) : base(element)
        {
            myValue = new Vector2();
        }
    }
}
