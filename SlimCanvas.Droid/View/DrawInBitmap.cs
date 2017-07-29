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
    internal class DrawInBitmap
    {
        Bitmap myBitmap;
        Android.Graphics.Canvas canvas;

        public DrawInBitmap(int width, int height)
        {
            myBitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            canvas = new Android.Graphics.Canvas(myBitmap);
        }

        public Bitmap GetAsBitmap()
        {
            canvas.Dispose();
            return myBitmap;
        }

        public void DrawBitmap(Bitmap bitmap, Matrix trans, BitmapInterpolationMode mode)
        {
            var paint = new Paint()
            {
                Alpha = 255
            };
            
            canvas.DrawBitmap(bitmap, trans, paint);
        }
    }
}