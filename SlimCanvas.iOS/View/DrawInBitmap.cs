using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;
using CoreText;

namespace SlimCanvas.iOS
{
    internal class DrawInBitmap : IDisposable
    {
        CGColorSpace cp;
        public CGBitmapContext ctx;
        byte[] _pixelArray;
        int Width;
        int Height;
        
        public DrawInBitmap(int width, int height)
        {
            int len = width * height;
            int count = len * 4;
            _pixelArray = new byte[count];

            Width = width;
            Height = height;

            cp = CGColorSpace.CreateDeviceRGB();
            ctx = new CGBitmapContext(_pixelArray, width, height, 8, (int)(width * 4), cp, CGImageAlphaInfo.PremultipliedLast);
        }

        public DrawInBitmap(byte[] pixelArray, int width, int height)
        {
            int len = width * height;
            int count = len * 4;
            _pixelArray = pixelArray;

            Width = width;
            Height = height;

            cp = CGColorSpace.CreateDeviceRGB();
            ctx = new CGBitmapContext(_pixelArray, width, height, 8, (int)(width * 4), cp, CGImageAlphaInfo.PremultipliedLast);
        }

        #region Clear

        public void Clear(Color color)
        {
            ctx.SetFillColor(LocalTransform.ToColor(color));
            ctx.FillRect(new CGRect(0, 0, ctx.Width, ctx.Height));
        }

        #endregion

        #region DrawPath

        public void DrawPath(List<Vector2> pointList, Color strokeCollor, double thickness, View.Controls.EnumTypes.DashStyle dashStyle, float[] dashPattern)
        {
            ctx.SaveState();

            var p = new CGPath();

            for (int i = 0; i < pointList.Count; i++)
            {
                var point = pointList[i];

                if (i == 0)
                    p.MoveToPoint((nfloat)(point.X), (nfloat)(point.Y));
                else
                    p.AddLineToPoint((nfloat)(point.X), (nfloat)(point.Y));
            }

            ctx.ScaleCTM(1, -1);
            ctx.TranslateCTM(0, -Height);
            ctx.AddPath(p);
            SetStroke(strokeCollor, thickness, dashStyle, dashPattern);
            ctx.StrokePath();

            ctx.RestoreState();
        }

        #endregion
        
        #region SetStroke

        void SetStroke(Color color, double thickness, View.Controls.EnumTypes.DashStyle strokeStyle, float[] dashPattern)
        {
            if (color.A > 0 && thickness > 0)
            {
                ctx.SetLineWidth((nfloat)thickness);
                ctx.SetStrokeColor(LocalTransform.ToColor(color));

                float dot = (float)thickness;
                float dash = dot * 2;

                switch (strokeStyle)
                {
                    case View.Controls.EnumTypes.DashStyle.Dash:
                        ctx.SetLineDash(0, new nfloat[] { dash, dash });
                        break;
                    case View.Controls.EnumTypes.DashStyle.Dot:
                        ctx.SetLineDash(0, new nfloat[] { dot, dot });
                        break;
                    case View.Controls.EnumTypes.DashStyle.DashDotDot:
                        ctx.SetLineDash(0, new nfloat[] { dash, dash, 0, dash });
                        break;
                    case View.Controls.EnumTypes.DashStyle.Custom:
                        var pattern = dashPattern.Select(p => (nfloat)p).ToArray();
                        ctx.SetLineDash(0, pattern);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region SetBrush

        void SetBrush(View.Brush brush, CGRect rect)
        {
            if (brush is View.SolidColorBrush c)
            {
                ctx.SetFillColor(LocalTransform.ToColor(c.Color));
                ctx.FillRect(rect);
            }
            else if (brush is View.LinearGradientBrush lb)
            {
                var gg = GetGradient(lb.Stops);

                var start = new CGPoint(rect.X, rect.Y + rect.Height);
                var end = new CGPoint(rect.X + rect.Width, rect.Y);
                ctx.DrawLinearGradient(gg, start, end, CGGradientDrawingOptions.None);
            }
            else if (brush is View.RadialGradientBrush rb)
            {
                var gg = GetGradient(rb.Stops);

                var centerX = (rect.X + rect.Width) / 2 + rb.GradientOriginOffset.X;
                var centerY = (rect.Y + rect.Height) / 2 + rb.GradientOriginOffset.Y;
                var start = new CGPoint(centerX, centerY);
                ctx.DrawRadialGradient(gg, start, 0, start, (nfloat)rb.Radius, CGGradientDrawingOptions.None);
            }
        }

        CGGradient GetGradient(IList<View.GradientStop> stops)
        {
            var c = stops.Count;
            var ls = new nfloat[c];
            var cl = new nfloat[c * 4];

            int cIndex = 0;
            for (int i = 0; i < c; i++)
            {
                var s = stops[i];
                ls[i] = (nfloat)s.Position;
                cl[cIndex] = s.Color.R;
                cl[cIndex + 1] = s.Color.G;
                cl[cIndex + 2] = s.Color.B;
                cl[cIndex + 3] = s.Color.A;

                cIndex += 4;
            }

            var cs = CGColorSpace.CreateDeviceRGB();
            return new CGGradient(cs, cl, ls);
        }

        #endregion

        public CGImage GetAsImage()
        {
            return ctx.ToImage();
        }

        public byte[] GetPixels()
        {
            return _pixelArray;
        }

        public void Dispose()
        {
            ctx.Dispose();
            cp.Dispose();
        }
    }
}