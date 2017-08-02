using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.UWP
{
    internal static class Shared
    {
        public static SharpDX.DirectWrite.Factory DirectWriteFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
        public static SharpDX.Direct2D1.Factory Direct2D1Factory = new SharpDX.Direct2D1.Factory();
        public static SharpDX.WIC.ImagingFactory WicImagingFactory = new SharpDX.WIC.ImagingFactory();
        public static SharpDX.Direct2D1.DeviceContext D2dContext;
    }
}
