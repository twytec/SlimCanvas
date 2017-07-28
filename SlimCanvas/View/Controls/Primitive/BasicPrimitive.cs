using SlimCanvas.View.Controls.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    public class BasicPrimitive : UIElement
    {
        #region Get

        public Propertys.StrokeColorProperty StrokeColorProperty { get { return _StrokeColorProperty; } }
        public Propertys.ThicknessProperty ThicknessProperty { get { return _ThicknessProperty; } }
        public Propertys.StrokeStyleProperty StrokeStyleProperty { get { return _StrokeStyleProperty; } }
        public Propertys.DashPatternProperty DashPatternProperty { get { return _DashPatternProperty; } }

        #endregion

        internal Propertys.StrokeColorProperty _StrokeColorProperty;
        internal Propertys.ThicknessProperty _ThicknessProperty;
        internal Propertys.StrokeStyleProperty _StrokeStyleProperty;
        internal Propertys.DashPatternProperty _DashPatternProperty;

        public BasicPrimitive()
        {
            _StrokeColorProperty = new Propertys.StrokeColorProperty(this);
            _ThicknessProperty = new Propertys.ThicknessProperty(this);
            _StrokeStyleProperty = new Propertys.StrokeStyleProperty(this);
            _DashPatternProperty = new Propertys.DashPatternProperty(this);
        }

        public Color StrokeColor
        {
            get { return (Color)_StrokeColorProperty.GetValue(); }
            set { _StrokeColorProperty.SetValue(value); }
        }

        public DashStyle StrokeStyle
        {
            get { return (DashStyle)_StrokeStyleProperty.GetValue(); }
            set { _StrokeStyleProperty.SetValue(value); }
        }

        /// <summary>
        /// Set StrokeStyle to custom
        /// </summary>
        public float[] DashPattern
        {
            get { return (float[])_DashPatternProperty.GetValue(); }
            set { _DashPatternProperty.SetValue(value); }
        }

        public double Thickness
        {
            get { return (double)_ThicknessProperty.GetValue(); }
            set { _ThicknessProperty.SetValue(value); }
        }
    }
}
