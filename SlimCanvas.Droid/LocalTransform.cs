using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace SlimCanvas.Droid
{
    internal static class LocalTransform
    {
        public static Android.Graphics.Color ToColor(Color color)
        {
            return Android.Graphics.Color.Argb(color.A, color.R, color.G, color.B);
        }

        public static int ToIntColorArgb(Color color)
        {
            return (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
        }

        public static Android.Graphics.Rect ToRect(double x, double y, double width, double height)
        {
            return new Android.Graphics.Rect((int)x, (int)y, (int)(x + width), (int)(y + height));
        }

        public static Android.Graphics.Rect ToRect(Rect rect)
        {
            return new Android.Graphics.Rect((int)rect.X, (int)rect.Y, (int)(rect.Width + rect.X), (int)(rect.Height + rect.Y));
        }

        public static Android.Graphics.RectF ToRectF(double x, double y, double width, double height)
        {
            return new Android.Graphics.RectF((float)x, (float)y, (float)(x + width), (float)(y + height));
        }

        public static Matrix GetMatrix(double rotation, Vector2 center, Vector2 scal, Vector2 pos)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate((float)rotation, (float)(center.X), (float)(center.Y));
            matrix.PostScale((float)scal.X, (float)scal.Y);
            matrix.PostTranslate((float)(pos.X), (float)(pos.Y));

            return matrix;
        }
    }
}