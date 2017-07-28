using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.UWP
{
    internal static class LocalTransform
    {
        public static Size GetSize(double width, double height)
        {
            var display = Windows.Graphics.Display.DisplayInformation.GetForCurrentView();

            double w = 0;
            double h = 0;

            if (display.CurrentOrientation == Windows.Graphics.Display.DisplayOrientations.Landscape || display.CurrentOrientation == Windows.Graphics.Display.DisplayOrientations.LandscapeFlipped)
            {
                w = width;
                h = height;
            }
            else if (display.CurrentOrientation == Windows.Graphics.Display.DisplayOrientations.Portrait || display.CurrentOrientation == Windows.Graphics.Display.DisplayOrientations.PortraitFlipped)
            {
                h = width;
                w = height;
            }

            return new Size(w * display.RawPixelsPerViewPixel, h * display.RawPixelsPerViewPixel);
        }

        public static SharpDX.Color ToColor(Color color)
        {
            return new SharpDX.Color(color.R, color.G, color.B, color.A);
        }

        public static SharpDX.Mathematics.Interop.RawVector2 ToVector2(SlimCanvas.Vector2 vec)
        {
            return new SharpDX.Mathematics.Interop.RawVector2((float)vec.X, (float)vec.Y);
        }

        public static SharpDX.RectangleF ToRectangleF(Rect rect)
        {
            return new SharpDX.RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        static SharpDX.Vector2 myCenter;
        static SharpDX.Matrix3x2 myRotation;
        static SharpDX.Matrix3x2 myScale;
        static SharpDX.Matrix3x2 myTrans;

        public static SharpDX.Matrix3x2 ToMartix3x2(Vector2 center, double degrees, Vector2 scale, double x, double y)
        {
            myCenter = ToVector2(center);
            myRotation = SharpDX.Matrix3x2.Rotation(SharpDX.MathUtil.DegreesToRadians((float)degrees), myCenter);
            myScale = SharpDX.Matrix3x2.Scaling((float)scale.X, (float)scale.Y);
            myTrans = SharpDX.Matrix3x2.Translation((float)x, (float)y);

            return (myScale * myRotation * myTrans);
        }
    }
}
