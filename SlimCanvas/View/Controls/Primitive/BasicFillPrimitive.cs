using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    public class BasicFillPrimitive : BasicPrimitive
    {
        public BasicFillPrimitive()
        {
            _FillBrushProperty = new Propertys.FillBrushProperty(this);
            
        }

        public Propertys.FillBrushProperty FillBrushProperty { get { return _FillBrushProperty; } }

        internal Propertys.FillBrushProperty _FillBrushProperty;

        public Brush FillBrush
        {
            get { return (Brush)_FillBrushProperty.GetValue(); }
            set { _FillBrushProperty.SetValue(value); }
        }

        
    }
}
