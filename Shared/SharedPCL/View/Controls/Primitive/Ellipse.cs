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
    public class Ellipse : BasicFillPrimitive
    {
        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse()
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse(Vector2 startPoint, int radius) 
            : this (startPoint, radius, Color.Black, 1)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse(Vector2 startPoint, int radius, Color strokeColor, double thickness) 
            : this (startPoint, radius, strokeColor, thickness, EnumTypes.DashStyle.Solid)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse(Vector2 startPoint, int radius, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle)
            : this (startPoint, radius, strokeColor, thickness, strokeStyle, null)
        {
            
        }

        /// <summary>
        /// to be added
        /// </summary>
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

        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse(Rect rect) : this (rect, Color.Black, 1)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse(Rect rect, Color strokeColor, double thickness)
            : this (rect, strokeColor, thickness, EnumTypes.DashStyle.Solid)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Ellipse(Rect rect, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle)
            : this (rect, strokeColor, thickness, strokeStyle, null)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
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
