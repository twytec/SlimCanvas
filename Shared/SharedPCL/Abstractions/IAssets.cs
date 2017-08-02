using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    /// <summary>
    /// Interface to Assets an local folder
    /// </summary>
    public interface IAssets
    {
        #region Temp

        /// <summary>
        /// On UWP is Windows.Storage.ApplicationData.Current.TemporaryFolder
        /// On Android is context.CacheDir
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<System.IO.Stream> GetFileFromTempAsync(string filePath);

        /// <summary>
        /// On UWP is Windows.Storage.ApplicationData.Current.TemporaryFolder
        /// On Android is context.CacheDir
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task SaveFileToTempAsync(string fileName, System.IO.Stream stream);

        #endregion

        #region Assets folder

        /// <summary>
        /// On UWP is Assets folder
        /// On Android is Assets folder. Set image to AndroidAssets
        /// On iOS is Resources folder. Set build action to BundleResource
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<System.IO.Stream> GetFileFromAssetsAsync(string filePath);

        #endregion

        #region LocalFolder

        /// <summary>
        /// On UWP is Windows.Storage.ApplicationData.Current.LocalFolder
        /// On Android is context.ApplicationInfo.DataDir
        /// On iOS is NSBundle.MainBundle.BundlePath
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<System.IO.Stream> GetFileFromLocalFolderAsync(string filePath);

        /// <summary>
        /// On UWP is Windows.Storage.ApplicationData.Current.LocalFolder
        /// On Android is context.ApplicationInfo.DataDir
        /// On iOS is NSBundle.MainBundle.BundlePath
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task SaveFileToLocalFolderAsync(string fileName, System.IO.Stream stream);

        #endregion
    }
}
