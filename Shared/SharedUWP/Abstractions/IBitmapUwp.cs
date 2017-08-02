using SlimCanvas.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.UWP
{
    internal class IBitmapUwp : Abstractions.IBitmap
    {
        internal IBitmapUwp(SharpDX.WIC.BitmapSource bs)
        {
            Width = bs.Size.Width;
            Height = bs.Size.Height;
            
            BitmapSource = bs;
        }

        #region Propertys

        public int Width { get; private set; }
        public int Height { get; private set; }
        int elementId = -1;
        internal SharpDX.WIC.BitmapSource BitmapSource;

        #endregion

        #region Dispose

        public void Dispose()
        {
            BitmapSource.Dispose();
            if (elementId != -1)
                LocalCache.RemoveBitmap(elementId);
        }

        #endregion

        #region GetBitmap internal

        internal SharpDX.Direct2D1.Bitmap GetBitmap(SharpDX.Direct2D1.DeviceContext d2dContext, int id)
        {
            elementId = id;
            if (LocalCache.GetBitmap(id) is SharpDX.Direct2D1.Bitmap b)
            {
                return b;
            }
            else if (id == -1)
            {
                var bitmap = SharpDX.Direct2D1.Bitmap1.FromWicBitmap(d2dContext, BitmapSource);
                return bitmap;
            }
            else
            {
                var bitmap = SharpDX.Direct2D1.Bitmap1.FromWicBitmap(d2dContext, BitmapSource);
                LocalCache.AddBitmap(id, bitmap);
                return bitmap;
            }
        }

        #endregion

        #region GetPixels

        public byte[] GetPixels()
        {
            return GetPixels(new Rect(0, 0, Width, Height));
        }

        public byte[] GetPixels(Rect rect)
        {
            var stride = (int)rect.Width * 4;
            byte[] output = new byte[(int)rect.Height * stride];
            BitmapSource.CopyPixels(LocalTransform.ToRectangleF(rect), output, stride);

            return output;
        }

        #endregion

        #region SetPixel

        public void SetPixels(byte[] colors)
        {
            SetPixels(colors, Width);
        }

        public void SetPixels(byte[] colors, int width)
        {
            var lenght = colors.Length / 4;

            SharpDX.Color[] sc = new SharpDX.Color[lenght];
            int cIndex = 0;
            for (int i = 0; i < lenght; i++)
            {
                var r = colors[cIndex];
                var g = colors[cIndex + 1];
                var b = colors[cIndex + 2];
                var a = colors[cIndex + 3];
                sc[i] = new SharpDX.Color(r, g, b, a);

                cIndex += 4;
            }

            var nb = CreateImage(sc, width);
            Dispose();
            BitmapSource = nb.BitmapSource;
            Width = width;
            Height = lenght / Width;
        }

        #endregion

        #region CropBitmap

        public void CropBitmap(Rect rect)
        {
            var rot = SharpDX.Matrix3x2.Rotation(0);
            var scal = SharpDX.Matrix3x2.Scaling(new SharpDX.Vector2(1, 1));
            var trans = SharpDX.Matrix3x2.Translation(new SharpDX.Vector2((float)-rect.X, (float)-rect.Y));
            var matrix = scal * rot * trans;

            DrawInBitmap draw = new DrawInBitmap((int)rect.Width, (int)rect.Height);
            draw.DrawBitmap(BitmapSource, matrix, BitmapInterpolationMode.Linear);

            SharpDX.Utilities.Dispose(ref BitmapSource);

            BitmapSource = draw.GetAsBitmap();
            draw.Dispose();

            Width = (int)rect.Width;
            Height = (int)rect.Height;
        }

        #endregion

        #region ScaleBitmap

        public void ScaleBitmap(double newWidth, double newHeight, BitmapInterpolationMode mode)
        {
            var sX = newWidth / Width;
            var sY = newHeight / Height;

            var rot = SharpDX.Matrix3x2.Rotation(0);
            var scal = SharpDX.Matrix3x2.Scaling(new SharpDX.Vector2((float)sX, (float)sY));
            var trans = SharpDX.Matrix3x2.Translation(new SharpDX.Vector2());
            var matrix = scal * rot * trans;

            DrawInBitmap draw = new DrawInBitmap((int)(Width * sX), (int)(Height * sY));
            draw.DrawBitmap(BitmapSource, matrix, mode);

            SharpDX.Utilities.Dispose(ref BitmapSource);

            BitmapSource = draw.GetAsBitmap();
            draw.Dispose();

            Width = (int)newWidth;
            Height = (int)newHeight;
        }

        #endregion

        #region GetAsStream

        public async Task<System.IO.Stream> GetAsStreamAsync(BitmapEncoder encoder)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var stream = new System.IO.MemoryStream();
                    var wicStream = new SharpDX.WIC.WICStream(Shared.WicImagingFactory, stream);

                    SharpDX.WIC.BitmapEncoder encode;

                    if (encoder == BitmapEncoder.JPGE)
                        encode = new SharpDX.WIC.JpegBitmapEncoder(Shared.WicImagingFactory);
                    else
                        encode = new SharpDX.WIC.PngBitmapEncoder(Shared.WicImagingFactory);

                    encode.Initialize(wicStream);

                    var bitmapFrameEncode = new SharpDX.WIC.BitmapFrameEncode(encode);
                    bitmapFrameEncode.Initialize();
                    bitmapFrameEncode.SetSize(BitmapSource.Size.Width, BitmapSource.Size.Height);

                    var pixelFormatGuid = SharpDX.WIC.PixelFormat.FormatDontCare;
                    bitmapFrameEncode.SetPixelFormat(ref pixelFormatGuid);
                    bitmapFrameEncode.WriteSource(BitmapSource);

                    bitmapFrameEncode.Commit();
                    encode.Commit();

                    SharpDX.Utilities.Dispose(ref bitmapFrameEncode);
                    SharpDX.Utilities.Dispose(ref encode);
                    SharpDX.Utilities.Dispose(ref wicStream);

                    return stream;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        #endregion
        
        #region Static LoadBitmap, CreateImage

        public static Abstractions.IBitmap LoadBitmap(System.IO.Stream stream)
        {
            try
            {
                using (var d = new SharpDX.WIC.BitmapDecoder(Shared.WicImagingFactory, stream, SharpDX.WIC.DecodeOptions.CacheOnDemand))
                {
                    SharpDX.WIC.BitmapSource frame = d.GetFrame(0);
                    SharpDX.WIC.FormatConverter converter = new SharpDX.WIC.FormatConverter(Shared.WicImagingFactory);
                    converter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA);

                    var b = new SharpDX.WIC.Bitmap(Shared.WicImagingFactory, converter, SharpDX.WIC.BitmapCreateCacheOption.CacheOnLoad);
                    return new IBitmapUwp(b);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Abstractions.IBitmap CreateImage(byte[] colors, int width)
        {
            var lenght = colors.Length / 4;

            SharpDX.Color[] sc = new SharpDX.Color[lenght];
            int cIndex = 0;
            for (int i = 0; i < lenght; i++)
            {
                var r = colors[cIndex];
                var g = colors[cIndex + 1];
                var b = colors[cIndex + 2];
                var a = colors[cIndex + 3];
                sc[i] = new SharpDX.Color(r, g, b, a);

                cIndex += 4;
            }

            return CreateImage(sc, width);
        }

        public static IBitmapUwp CreateImage(SharpDX.Color[] colors, int width)
        {
            try
            {
                unsafe
                {
                    fixed (SharpDX.Color* p = colors)
                    {
                        var data = new SharpDX.DataRectangle
                        {
                            Pitch = width * 4,
                            DataPointer = (IntPtr)p,
                        };
                        var bitmap = new SharpDX.WIC.Bitmap(Shared.WicImagingFactory, width, colors.Length / width, SharpDX.WIC.PixelFormat.Format32bppPRGBA, data);
                        return new IBitmapUwp(bitmap);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #endregion
    }
}
