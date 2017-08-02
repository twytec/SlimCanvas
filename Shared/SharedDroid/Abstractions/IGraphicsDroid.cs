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
    internal class IGraphicsDroid : Abstractions.IGraphics
    {
        public Task<Abstractions.IBitmap> BitmapAsync(System.IO.Stream stream)
        {
            return Task.FromResult(IBitmapDroid.LoadBitmap(stream));
        }

        public Task<Abstractions.IBitmap> BitmapAsync(byte[] color, int width)
        {
            return Task.FromResult(IBitmapDroid.CreateImage(color, width));
        }
    }
}