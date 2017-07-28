using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class WidthProperty : BasicProperty
    {
        public WidthProperty(BasicElement element) : base(element)
        {
            myValue = 0d;
        }
    }
}
