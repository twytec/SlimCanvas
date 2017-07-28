using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class BackgroundProperty : Controls.Propertys.BasicProperty
    {
        public BackgroundProperty(UIElement element) : base(element)
        {
            myValue = SolidColorBrush.Black;
        }
    }
}
