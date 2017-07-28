using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using System.Threading.Tasks;

namespace SlimCanvas.iOS
{
    internal class IAssetsiOS : Abstractions.IAssets
    {
        public Task<Stream> GetFileFromTempAsync(string filePath)
        {
            return Task.Run(() =>
            {
                try
                {
                    var file = System.IO.Path.GetTempPath();
                    var path = Path.Combine(file, filePath);
                    return GetFileAsStream(path);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        public async Task SaveFileToTempAsync(string fileName, Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(ms);
                await ms.FlushAsync();

                var file = System.IO.Path.GetTempPath();
                var path = Path.Combine(file, fileName);
                File.WriteAllBytes(path, ms.ToArray());
            }
        }

        public Task<Stream> GetFileFromAssetsAsync(string filePath)
        {
            return Task.Run(() =>
            {
                try
                {
                    return GetFileAsStream(filePath);
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }

        public Task<Stream> GetFileFromLocalFolderAsync(string filePath)
        {
            return Task.Run(() =>
            {
                try
                {
                    var dd = NSBundle.MainBundle.BundlePath;
                    var path = Path.Combine(dd, filePath);
                    return GetFileAsStream(path);
                }
                catch (Exception)
                {

                    throw;
                }
            });
        }

        public async Task SaveFileToLocalFolderAsync(string fileName, Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(ms);
                await ms.FlushAsync();

                var file = NSBundle.MainBundle.BundlePath;
                var path = Path.Combine(file, fileName);
                File.WriteAllBytes(path, ms.ToArray());
            }
        }

        Stream GetFileAsStream(string path)
        {
            var s = File.OpenRead(path);
            return s;
        }
    }
}