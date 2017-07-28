using SlimCanvas.View.Controls.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class StrokeStyleProperty : Controls.Propertys.BasicProperty
    {
        public StrokeStyleProperty(UIElement element) : base(element)
        {
            myValue = DashStyle.Solid;
        }
    }
}
