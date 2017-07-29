using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    /// <summary>
    /// to be added
    /// </summary>
    public class BasicFillPrimitive : BasicPrimitive
    {
        /// <summary>
        /// to be added
        /// </summary>
        public BasicFillPrimitive()
        {
            _FillBrushProperty = new Propertys.FillBrushProperty(this);
            
        }

        /// <summary>
        /// to be added
        /// </summary>
        public Propertys.FillBrushProperty FillBrushProperty { get { return _FillBrushProperty; } }

        internal Propertys.FillBrushProperty _FillBrushProperty;

        /// <summary>
        /// to be added
        /// </summary>
        public Brush FillBrush
        {
            get { return (Brush)_FillBrushProperty.GetValue(); }
            set { _FillBrushProperty.SetValue(value); }
        }

        
    }
}
