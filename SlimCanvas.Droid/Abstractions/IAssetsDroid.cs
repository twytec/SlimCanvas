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
using System.IO;

namespace SlimCanvas.Droid
{
    internal class IAssetsDroid : Abstractions.IAssets
    {
        Context context;

        public IAssetsDroid(Context context)
        {
            this.context = context;
        }

        public async Task<Stream> GetFileFromTempAsync(string filePath)
        {
            try
            {
                var file = context.CacheDir;
                var path = Path.Combine(file.Path, filePath);
                return await GetFileAsStream(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveFileToTempAsync(string fileName, Stream stream)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(ms);
                    await ms.FlushAsync();

                    var temp = Path.GetTempPath();
                    var path = Path.Combine(temp, fileName);
                    File.WriteAllBytes(path, ms.ToArray());
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<Stream> GetFileFromAssetsAsync(string filePath)
        {
            try
            {
                var temp = Path.GetTempPath();
                var assetsDir = Path.Combine(temp, "SlimAssets");
                var file = Path.Combine(temp, assetsDir, filePath);

                if (!Directory.Exists(assetsDir))
                {
                    Directory.CreateDirectory(assetsDir);
                }

                if (!File.Exists(file))
                {
                    using (var asset = context.Resources.Assets.Open(filePath))
                    {
                        using (var dest = File.Create(file))
                        {
                            await asset.CopyToAsync(dest);
                        }
                    }
                }

                return await GetFileAsStream(file);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public Task<Stream> GetFileFromLocalFolderAsync(string filePath)
        {
            try
            {
                var dd = context.ApplicationInfo.DataDir;
                var path = Path.Combine(dd, filePath);
                return GetFileAsStream(path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveFileToLocalFolderAsync(string fileName, Stream stream)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(ms);
                    await ms.FlushAsync();

                    var file = context.ApplicationInfo.DataDir;
                    var path = Path.Combine(file, fileName);
                    File.WriteAllBytes(path, ms.ToArray());
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        Task<Stream> GetFileAsStream(string path)
        {
            return Task.FromResult<Stream>(File.OpenRead(path));
        }
    }
}