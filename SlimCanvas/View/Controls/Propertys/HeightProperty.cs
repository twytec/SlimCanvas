using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class HeightProperty : BasicProperty
    {
        public HeightProperty(BasicElement element) : base(element)
        {
            myValue = 0d;
        }
    }
}
