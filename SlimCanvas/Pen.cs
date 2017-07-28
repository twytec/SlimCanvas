using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    public class Pen
    {
        public Color Color { get; set; } = Color.Black;
        public double Width { get; set; } = 1;

        public Pen(Color color, double width)
        {
            Color = color;
            Width = width;
        }

        #region Static

        public static Pen Black = new Pen(Color.Black, 1);
        public static Pen Gray = new Pen(Color.Gray, 1);
        public static Pen White = new Pen(Color.White, 1);
        public static Pen Brown = new Pen(Color.Brown, 1);
        public static Pen Red = new Pen(Color.Red, 1);
        public static Pen Yellow = new Pen(Color.Yellow, 1);
        public static Pen Green = new Pen(Color.Green, 1);
        public static Pen Blue = new Pen(Color.Blue, 1);

        #endregion
    }
}
