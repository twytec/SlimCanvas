using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class DashPatternProperty : Controls.Propertys.BasicProperty
    {
        public DashPatternProperty(UIElement element) : base(element)
        {
            myValue = new float[] { };
        }
    }
}
