using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class StrokeColorProperty : Controls.Propertys.BasicProperty
    {
        public StrokeColorProperty(UIElement element) : base(element)
        {
            myValue = Color.Black;
        }
    }
}
