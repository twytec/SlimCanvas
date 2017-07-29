using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    /// <summary>
    /// Interface Bitamp
    /// </summary>
    public interface IBitmap
    {
        /// <summary>
        /// Size of Bitmap
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Size of Bitmap
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Dispose
        /// </summary>
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

        /// <summary>
        /// Get Bitmap as stream
        /// </summary>
        /// <param name="encoder"></param>
        /// <returns></returns>
        Task<System.IO.Stream> GetAsStreamAsync(BitmapEncoder encoder);

        /// <summary>
        /// Crop bitmap with rect
        /// </summary>
        /// <param name="rect"></param>
        void CropBitmap(Rect rect);

        /// <summary>
        /// Scale bitamp to new size
        /// </summary>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <param name="mode"></param>
        void ScaleBitmap(double newWidth, double newHeight, BitmapInterpolationMode mode);

    }
}
