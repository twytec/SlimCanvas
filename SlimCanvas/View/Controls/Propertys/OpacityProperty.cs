using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class OpacityProperty : BasicProperty
    {
        public OpacityProperty(UIElement element) : base(element)
        {
            myValue = 1d;
        }
    }
}
