using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace SlimCanvas.iOS
{
    internal class IGraphicsiOS : Abstractions.IGraphics
    {
        public Task<Abstractions.IBitmap> BitmapAsync(System.IO.Stream stream)
        {
            return Task.Run(() => IBitmapiOS.LoadBitmap(stream));
        }

        public Task<Abstractions.IBitmap> BitmapAsync(byte[] color, int width)
        {
            return Task.Run(() => IBitmapiOS.CreateImage(color, width));
        }
    }
}