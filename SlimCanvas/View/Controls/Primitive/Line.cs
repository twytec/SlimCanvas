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
    public class Line : BasicPrimitive
    {
        /// <summary>
        /// to be added
        /// </summary>
        public Propertys.EndPointProperty EndPointProperty { get { return _EndPointProperty; } }
        internal Propertys.EndPointProperty _EndPointProperty;

        /// <summary>
        /// to be added
        /// </summary>
        public Vector2 EndPoint
        {
            get { return (Vector2)_EndPointProperty.GetValue(); }
            set { _EndPointProperty.SetValue(value); }
        }

        /// <summary>
        /// to be added
        /// </summary>
        public Line()
            : this (new Vector2(), new Vector2())
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Line(Vector2 startPoint, Vector2 endPoint)
            : this (startPoint, endPoint, Color.Black, 1)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Line(Vector2 startPoint, Vector2 endPoint, Color strokeColor, double thickness)
        {
            _EndPointProperty = new Propertys.EndPointProperty(this);
            _EndPointProperty.PropertyChanged += _EndPointProperty_PropertyChanged;

            X = startPoint.X;
            Y = startPoint.Y;
            EndPoint = endPoint;
            StrokeColor = strokeColor;
            Thickness = thickness;

            
        }

        private void _EndPointProperty_PropertyChanged(object sender, EventArgs e)
        {
            var size = GeometryHelper.GetSize(new Vector2(X, Y), EndPoint);
            Width = size.Width;

            if (size.Height == 0)
                Height = Thickness;
            else
                Height = size.Height;
        }
    }
}
