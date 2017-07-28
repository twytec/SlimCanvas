using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class FillBrushProperty : Controls.Propertys.BasicProperty
    {
        public FillBrushProperty(UIElement element) : base(element)
        {
            myValue = SolidColorBrush.Black;
        }
    }
}
