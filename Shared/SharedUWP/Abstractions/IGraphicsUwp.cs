using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.Abstractions;
using SlimCanvas.View.Controls;

namespace SlimCanvas.UWP
{
    internal class IGraphicsUwp : IGraphics
    {
        public Task<Abstractions.IBitmap> BitmapAsync(Stream stream)
        {
            return Task.Run(() => IBitmapUwp.LoadBitmap(stream));
        }

        public Task<Abstractions.IBitmap> BitmapAsync(byte[] color, int width)
        {
            return Task.Run(() => IBitmapUwp.CreateImage(color, width));
        }
    }
}
