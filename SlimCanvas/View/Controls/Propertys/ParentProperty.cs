using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class ParentProperty : BasicProperty
    {
        public ParentProperty(BasicElement element) : base(element)
        {
            myValue = null;
        }
    }
}
