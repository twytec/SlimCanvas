using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.UWP
{
    internal class IAssetsUwp : Abstractions.IAssets
    {
        public async Task<Stream> GetFileFromTempAsync(string filePath)
        {
            var temp = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            var file = await temp.GetFileAsync(filePath);
            var stream = await file.OpenReadAsync();
            return stream.AsStream();
        }

        public async Task<Stream> GetFileFromAssetsAsync(string filePath)
        {
            var assets = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var file = await assets.GetFileAsync(filePath);
            var stream = await file.OpenReadAsync();
            return stream.AsStream();
        }

        public async Task SaveFileToTempAsync(string fileName, Stream stream)
        {
            var temp = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            var file = await temp.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                using (var s = fileStream.AsStreamForWrite())
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(s);
                    await s.FlushAsync();
                }
            }
        }

        public async Task<Stream> GetFileFromLocalFolderAsync(string filePath)
        {
            var temp = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await temp.GetFileAsync(filePath);
            var stream = await file.OpenReadAsync();
            return stream.AsStream();
        }

        public async Task SaveFileToLocalFolderAsync(string fileName, Stream stream)
        {
            var temp = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await temp.CreateFileAsync(fileName, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
            {
                using (var s = fileStream.AsStreamForWrite())
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    await stream.CopyToAsync(s);
                    await s.FlushAsync();
                }
            }
        }
    }
}
