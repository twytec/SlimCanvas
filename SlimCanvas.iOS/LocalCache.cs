using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace SlimCanvas.iOS
{
    internal static class LocalCache
    {
        static Dictionary<string, CGImage> textCache = new Dictionary<string, CGImage>();
        static Dictionary<int, CGImage> pathCache = new Dictionary<int, CGImage>();

        #region Text

        public static CGImage GetText(string key)
        {
            if (textCache.ContainsKey(key))
            {
                return textCache[key];
            }

            return null;
        }

        public static void AddText(CGImage img, string key)
        {
            textCache.Add(key, img);
        }

        public static void RemoveText(string key)
        {
            if (textCache.ContainsKey(key))
            {
                textCache[key].Dispose();
                textCache.Remove(key);
            }
        }

        #endregion

        #region Path

        public static CGImage GetPath(int key)
        {
            if (pathCache.ContainsKey(key))
            {
                return pathCache[key];
            }

            return null;
        }

        public static void AddPath(CGImage img, int key)
        {
            pathCache.Add(key, img);
        }

        public static void RemovePath(int key)
        {
            if (pathCache.ContainsKey(key))
            {
                pathCache[key].Dispose();
                pathCache.Remove(key);
            }
        }

        #endregion
    }
}