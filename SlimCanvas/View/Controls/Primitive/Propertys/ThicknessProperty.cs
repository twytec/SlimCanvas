using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class ThicknessProperty : Controls.Propertys.BasicProperty
    {
        public ThicknessProperty(UIElement element) : base(element)
        {
            myValue = 1d;
        }
    }
}
