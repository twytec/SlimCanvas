using SlimCanvas.View.Controls.EnumTypes;
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
    public class StrokeStyleProperty : Controls.Propertys.BasicProperty
    {
        /// <summary>
        /// to be added
        /// </summary>
        public StrokeStyleProperty(UIElement element) : base(element)
        {
            myValue = DashStyle.Solid;
        }
    }
}
