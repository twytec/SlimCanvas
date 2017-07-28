using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    public interface IBitmap
    {
        int Width { get; }
        int Height { get; }
        void Dispose();

        /// <summary>
        /// Format is RGBA
        /// </summary>
        /// <returns></returns>
        byte[] GetPixels();

        /// <summary>
        /// Format is RGBA
        /// </summary>
        /// <returns></returns>
        byte[] GetPixels(Rect rect);

        /// <summary>
        /// Format is RGBA
        /// </summary>
        /// <returns></returns>
        void SetPixels(byte[] colors);

        /// <summary>
        /// Format is RGBA
        /// </summary>
        /// <returns></returns>
        void SetPixels(byte[] colors, int width);

        Task<System.IO.Stream> GetAsStreamAsync(BitmapEncoder encoder);

        void CropBitmap(Rect rect);

        void ScaleBitmap(double newWidth, double newHeight, BitmapInterpolationMode mode);

    }
}
