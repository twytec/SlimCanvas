using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive.Propertys
{
    /// <summary>
    /// to be added
    /// </summary>
    public class StartPointProperty : Controls.Propertys.BasicProperty
    {
        /// <summary>
        /// to be added
        /// </summary>
        public StartPointProperty(UIElement element) : base(element)
        {
            myValue = new Vector2();
        }
    }
}
