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
using System.Threading.Tasks;

namespace SlimCanvas.Droid
{
    public class IBitmapDroid : Abstractions.IBitmap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        internal Android.Graphics.Bitmap myBitmap;

        internal IBitmapDroid(Android.Graphics.Bitmap bm)
        {
            Width = bm.Width;
            Height = bm.Height;
            myBitmap = bm;
        }

        public void Dispose()
        {
            myBitmap.Dispose();
        }

        #region GetPixels

        public byte[] GetPixels()
        {
            return GetPixels(new Rect(0, 0, Width, Height));
        }

        public byte[] GetPixels(Rect rect)
        {
            int count = (int)(rect.Width * rect.Height);

            int[] output = new int[count];
            myBitmap.GetPixels(output, 0, (int)rect.Width, (int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);

            byte[] pixels = new byte[count * 4];

            int cIndex = 0;
            for (int i = 0; i < output.Length; i++)
            {
                var c = output[i];
                var a = Android.Graphics.Color.GetAlphaComponent(c);
                var r = Android.Graphics.Color.GetRedComponent(c);
                var g = Android.Graphics.Color.GetGreenComponent(c);
                var b = Android.Graphics.Color.GetBlueComponent(c);

                pixels[cIndex] = (byte)r;
                pixels[cIndex + 1] = (byte)g;
                pixels[cIndex + 2] = (byte)b;
                pixels[cIndex + 3] = (byte)a;

                cIndex += 4;
            }
            output = null;
            return pixels;
        }

        #endregion

        #region SetPixels

        public void SetPixels(byte[] colors)
        {
            SetPixels(colors, Width);
        }

        public void SetPixels(byte[] colors, int width)
        {
            int[] pi = FromRgbaToArgb(colors);
            myBitmap.SetPixels(pi, 0, (int)(width * 4), 0, 0, width, (int)(colors.Length / width));
        }

        #endregion

        #region GetAsStream

        public async Task<System.IO.Stream> GetAsStreamAsync(BitmapEncoder encoder)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            if (encoder == BitmapEncoder.JPGE)
            {
                await myBitmap.CompressAsync(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, ms);
            }
            else
            {
                await myBitmap.CompressAsync(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);
            }

            return ms;
        }

        #endregion

        #region CropBitmap

        public void CropBitmap(Rect rect)
        {
            var martix = LocalTransform.GetMatrix(0, new Vector2(), new Vector2(1, 1), new Vector2(-rect.X, -rect.Y));

            DrawInBitmap draw = new DrawInBitmap((int)rect.Width, (int)rect.Height);
            draw.DrawBitmap(myBitmap, martix, BitmapInterpolationMode.Linear);

            myBitmap.Recycle();
            myBitmap = draw.GetAsBitmap();

            Width = myBitmap.Width;
            Height = myBitmap.Height;
        }

        #endregion

        #region ScaleBitmap

        public void ScaleBitmap(double newWidth, double newHeight, BitmapInterpolationMode mode)
        {
            var sX = newWidth / Width;
            var sY = newHeight / Height;

            var martix = LocalTransform.GetMatrix(0, new Vector2(), new Vector2(sX, sY), new Vector2());

            DrawInBitmap draw = new DrawInBitmap((int)(Width * sX), (int)(Height * sY));
            draw.DrawBitmap(myBitmap, martix, BitmapInterpolationMode.Linear);

            myBitmap.Recycle();
            myBitmap = draw.GetAsBitmap();

            Width = myBitmap.Width;
            Height = myBitmap.Height;
        }

        #endregion

        #region LoadBitmap CreateImage

        public static Abstractions.IBitmap LoadBitmap(System.IO.Stream stream)
        {
            var bm = Android.Graphics.BitmapFactory.DecodeStream(stream);

            if (bm == null)
            {
                throw new InvalidOperationException("Can not create Bitmap from this Stream");
            }

            return new IBitmapDroid(bm);
        }

        public static Abstractions.IBitmap CreateImage(byte[] colors, int width)
        {
            var pixel = FromRgbaToArgb(colors);
            var bitmap = Android.Graphics.Bitmap.CreateBitmap(pixel, width, (int)(colors.Length / width), Android.Graphics.Bitmap.Config.Argb8888);

            if (bitmap == null)
            {
                throw new InvalidOperationException("Can not create Bitmap");
            }

            return new IBitmapDroid(bitmap);
        }

        #endregion

        #region Helper

        static int[] FromRgbaToArgb(byte[] colors)
        {
            int[] pi = new int[colors.Length];
            for (int i = 0; i < colors.Length; i += 4)
            {
                //From RGBA to ARGB
                pi[i] = colors[i + 3];
                pi[i + 1] = colors[i];
                pi[i + 2] = colors[i + 1];
                pi[i + 3] = colors[i + 2];
            }

            return pi;
        }

        #endregion
    }
}