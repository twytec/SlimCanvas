using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    /// <summary>
    /// Graphics interface
    /// </summary>
    public interface IGraphics
    {
        /// <summary>
        /// Only JPG,PNG
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<IBitmap> BitmapAsync(System.IO.Stream stream);

        /// <summary>
        /// PixelFormat RBGA
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        Task<IBitmap> BitmapAsync(byte[] color, int width);
    }
}
