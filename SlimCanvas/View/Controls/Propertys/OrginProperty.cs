using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    /// <summary>
    /// to be added
    /// </summary>
    public class OriginProperty : BasicProperty
    {

        /// <summary>
        /// to be added
        /// </summary>
        public OriginProperty(UIElement element) : base(element)
        {
            myValue = new Vector2();
        }
    }
}
