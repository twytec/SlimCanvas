using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    public class EndPointProperty : Controls.Propertys.BasicProperty
    {
        public EndPointProperty(UIElement element) : base(element)
        {
            myValue = new Vector2();
        }
    }
}
