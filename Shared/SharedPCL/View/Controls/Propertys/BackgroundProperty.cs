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
    public class BackgroundProperty : Controls.Propertys.BasicProperty
    {
        /// <summary>
        /// to be added
        /// </summary>
        public BackgroundProperty(UIElement element) : base(element)
        {
            myValue = new SolidColorBrush(Color.Black);
        }
    }
}
