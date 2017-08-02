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
    public class Rectangle : BasicFillPrimitive
    {
        /// <summary>
        /// to be added
        /// </summary>
        public Rectangle()
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Rectangle(Rect rect) : this (rect, Color.Black, 1)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Rectangle(Rect rect, Color strokeColor, double thickness)
            : this (rect, strokeColor, thickness, EnumTypes.DashStyle.Solid)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
        public Rectangle(Rect rect, Color strokeColor, double thickness, EnumTypes.DashStyle strokeStyle)
            : this (rect, strokeColor, thickness, strokeStyle, null)
        {

        }

        /// <summary>
        /// to be added
        /// </summary>
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
