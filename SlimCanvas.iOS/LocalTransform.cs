using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace SlimCanvas.iOS
{
    internal static class LocalTransform
    {
        public static CGColor ToColor(Color color)
        {
            return new CGColor(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
        }

        public static nfloat DegreesToRadians(double degrees)
        {
            var rad = 0.0174533d;
            return (nfloat)(degrees * rad);
        }

        public static CGRect ToCGRect(Rect rect)
        {
            return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}