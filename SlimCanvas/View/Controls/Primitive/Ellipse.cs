using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    public class Ellipse : BasicFillPrimitive
    {
        public Ellipse()
        {

        }

        public Ellipse(Vector2 startPoint, int radius) 
            : this (startPoint, radius, Color.Black, 1)
        {

        }

        public Ellipse(Vector2 startPoint, int radius, Color strokeColor, double thickness) 
            : this (startPoint, radius, strokeColor, thickness, EnumTypes.DashStyle.Solid)
        {

        }

        public Ellipse(Vector2 startPoint, int radius, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle)
            : this (startPoint, radius, strokeColor, thickness, strokeStyle, null)
        {
            
        }

        public Ellipse(Vector2 startPoint, int radius, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle, Brush fillColor)
        {
            var size = radius * 2;

            X = startPoint.X;
            Y = startPoint.Y;
            Width = size;
            Height = size;
            StrokeColor = strokeColor;
            Thickness = thickness;
            StrokeStyle = strokeStyle;
            FillBrush = fillColor;
        }

        public Ellipse(Rect rect) : this (rect, Color.Black, 1)
        {

        }

        public Ellipse(Rect rect, Color strokeColor, double thickness)
            : this (rect, strokeColor, thickness, EnumTypes.DashStyle.Solid)
        {

        }

        public Ellipse(Rect rect, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle)
            : this (rect, strokeColor, thickness, strokeStyle, null)
        {

        }

        public Ellipse(Rect rect, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle, Brush fillColor)
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
