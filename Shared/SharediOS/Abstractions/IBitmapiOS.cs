using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using CoreGraphics;

namespace SlimCanvas.iOS
{
    internal class IBitmapiOS : Abstractions.IBitmap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        internal CGImage MyImage;

        internal IBitmapiOS(CGImage img)
        {
            MyImage = img;
            Width = (int)img.Width;
            Height = (int)img.Height;
        }

        public void Dispose()
        {
            if (MyImage != null)
                MyImage.Dispose();
        }

        #region GetPixel

        public byte[] GetPixels()
        {
            return GetPixels(new Rect(0, 0, Width, Height));
        }

        public byte[] GetPixels(Rect rect)
        {
            using (var d = new DrawInBitmap((int)rect.Width, (int)rect.Height))
            {
                if (rect.X > 0 || rect.Y > 0)
                {
                    d.ctx.TranslateCTM(-(nfloat)rect.X, -(nfloat)(rect.Height - rect.Y));
                }
                
                d.ctx.DrawImage(new CGRect(0, 0, Width, Height), MyImage);
                return d.GetPixels();
            }
        }

        #endregion

        #region SetPixel

        public void SetPixels(byte[] colors)
        {
            SetPixels(colors, Width);
        }

        public void SetPixels(byte[] colors, int width)
        {
            int height = colors.Length / width;

            using (var ctx = new DrawInBitmap(colors, width, height))
            {
                MyImage = ctx.GetAsImage();
            }
        }

        #endregion

        #region CropBitmap

        public void CropBitmap(Rect rect)
        {
            using (var d = new DrawInBitmap((int)rect.Width, (int)rect.Height))
            {
                if (rect.X > 0 || rect.Y > 0)
                {
                    d.ctx.TranslateCTM(-(nfloat)rect.X, -(nfloat)(rect.Height - rect.Y));
                }

                d.ctx.DrawImage(new CGRect(0, 0, Width, Height), MyImage);

                if (MyImage != null)
                    MyImage.Dispose();

                MyImage = d.GetAsImage();
                Width = (int)MyImage.Width;
                Height = (int)MyImage.Height;
            }
        }

        #endregion

        #region GetAsStreamAsync

        public Task<Stream> GetAsStreamAsync(BitmapEncoder encoder)
        {
            if (encoder == BitmapEncoder.JPGE)
            {
                return Task.FromResult(UIImage.FromImage(MyImage).AsJPEG().AsStream());
            }
            else
            {
                return Task.FromResult(UIImage.FromImage(MyImage).AsPNG().AsStream());
            }
        }

        #endregion

        #region ScaleBitmap

        public void ScaleBitmap(double newWidth, double newHeight, BitmapInterpolationMode mode)
        {
            using (var d = new DrawInBitmap((int)Width, (int)Height))
            {
                
                var scaleX = Width / newWidth;
                var scaleY = Height / newHeight;

                d.ctx.ScaleCTM((nfloat)scaleX, (nfloat)scaleY);
                d.ctx.DrawImage(new CGRect(0, 0, newWidth, newHeight), MyImage);

                if (MyImage != null)
                    MyImage.Dispose();

                MyImage = d.GetAsImage();
                Width = (int)newWidth;
                Height = (int)newHeight;
            }
        }

        #endregion

        #region LoadBitmap CreateImage

        public static Abstractions.IBitmap LoadBitmap(System.IO.Stream stream)
        {
            var mem = new MemoryStream((int)stream.Length);
            stream.CopyTo(mem);
            unsafe
            {
                fixed (byte* x = mem.GetBuffer())
                {
                    var provider = new CGDataProvider(new IntPtr(x), (int)mem.Length, false);
                    var image = CGImage.FromPNG(provider, null, false, CGColorRenderingIntent.Default);
                    return new IBitmapiOS(image);
                }
            }
        }

        public static Abstractions.IBitmap CreateImage(byte[] colors, int width)
        {
            int height = colors.Length / 4 / width;

            using (var ctx = new DrawInBitmap(colors, width, height))
            {
                var img = ctx.GetAsImage();
                return new IBitmapiOS(img);
            }
        }

        #endregion
    }
}