using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.IO;
using SharpDX.WIC;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.WIC.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using SharpDX.Mathematics.Interop;

namespace SlimCanvas.UWP
{
    internal class DrawInBitmap
    {
        WicRenderTarget rt;
        Bitmap wicBitmap;

        int width;
        int height;

        public DrawInBitmap(int pixelWidth, int pixelHeight)
        {
            width = pixelWidth;
            height = pixelHeight;

            wicBitmap = new Bitmap(Shared.WicImagingFactory, pixelWidth, pixelHeight, SharpDX.WIC.PixelFormat.Format32bppPRGBA, BitmapCreateCacheOption.CacheOnLoad);

            var renderTargetProperties =
                new RenderTargetProperties(
                    RenderTargetType.Default,
                    new PixelFormat(Format.Unknown,
                    AlphaMode.Unknown), 0, 0, RenderTargetUsage.None, FeatureLevel.Level_DEFAULT);

            rt = new WicRenderTarget(Shared.Direct2D1Factory, wicBitmap, renderTargetProperties);
            rt.BeginDraw();
        }

        public void Dispose()
        {
            Utilities.Dispose(ref rt);
        }

        #region GetAsBitmap

        public Bitmap GetAsBitmap()
        {
            rt.EndDraw();
            return wicBitmap;
        }

        #endregion

        public void DrawGeometry(Geometry g, Matrix3x2 trans, StrokeStyle ss, SolidColorBrush strokeColor, float strokeWidth, Brush FillBrush)
        {
            rt.Transform = trans;

            if (FillBrush != null)
            {
                rt.FillGeometry(g, FillBrush);
            }


            rt.DrawGeometry(g, strokeColor, strokeWidth, ss);

            rt.Transform = Matrix3x2.Identity;
        }

        public void DrawBitmap(BitmapSource bitmap, Matrix3x2 trans, SharpDX.RectangleF rect, float opacity)
        {
            var b = Bitmap1.FromWicBitmap(rt, bitmap);
            rt.Transform = trans;
            rt.DrawBitmap(b, rect, opacity, SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor, rect);
            rt.Transform = Matrix3x2.Identity;
        }

        public void DrawBitmap(BitmapSource bitmap, Matrix3x2 trans, BitmapInterpolationMode mode)
        {
            var b = Bitmap1.FromWicBitmap(rt, bitmap);

            SharpDX.Direct2D1.BitmapInterpolationMode bm;

            if (mode == BitmapInterpolationMode.Linear)
                bm = SharpDX.Direct2D1.BitmapInterpolationMode.Linear;
            else
                bm = SharpDX.Direct2D1.BitmapInterpolationMode.NearestNeighbor;

            rt.Transform = trans;
            rt.DrawBitmap(b, 1, bm);
            rt.Transform = Matrix3x2.Identity;
        }

        public void DrawText(SharpDX.DirectWrite.TextLayout tl, SolidColorBrush brush, Matrix3x2 trans)
        {
            rt.Transform = trans;
            rt.DrawTextLayout(new RawVector2(), tl, brush);
            rt.Transform = Matrix3x2.Identity;
        }
    }
}
