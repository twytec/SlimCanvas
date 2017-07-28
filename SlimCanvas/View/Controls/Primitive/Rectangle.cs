using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    public class Rectangle : BasicFillPrimitive
    {
        public Rectangle()
        {

        }

        public Rectangle(Rect rect) : this (rect, Color.Black, 1)
        {

        }

        public Rectangle(Rect rect, Color strokeColor, double thickness)
            : this (rect, strokeColor, thickness, EnumTypes.DashStyle.Solid)
        {

        }

        public Rectangle(Rect rect, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle)
            : this (rect, strokeColor, thickness, strokeStyle, null)
        {

        }

        public Rectangle(Rect rect, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle, Brush fillColor)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
            StrokeColor = strokeColor;
            Thickness = thickness;
            StrokeStyle = strokeStyle;
            FillBrush = fillColor;
        }
    }
}
