using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    public struct Rect
    {
        public static Rect Zero { get { return new Rect(0, 0, 0, 0); } }

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Rect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Rect)
            {
                return this == ((Rect)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * 5 + Y.GetHashCode() * 13 + Width.GetHashCode() * 19 + Height.GetHashCode() * 29;
        }

        public static bool operator == (Rect a, Rect b)
        {
            return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(Rect a, Rect b)
        {
            return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
        }
    }
}
