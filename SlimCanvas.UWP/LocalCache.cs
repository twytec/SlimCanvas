using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.UWP
{
    internal static class LocalCache
    {
        static Dictionary<int, SharpDX.Direct2D1.Bitmap> bitmapCache = new Dictionary<int, SharpDX.Direct2D1.Bitmap>();

        static Dictionary<int, SharpDX.Direct2D1.Geometry> pathCache = new Dictionary<int, SharpDX.Direct2D1.Geometry>();
        
        static Dictionary<string, SharpDX.Direct2D1.StrokeStyle> strokeCache = new Dictionary<string, SharpDX.Direct2D1.StrokeStyle>();
        static Dictionary<string, SharpDX.Direct2D1.Geometry> geometryCache = new Dictionary<string, SharpDX.Direct2D1.Geometry>();

        static Dictionary<string, SharpDX.Direct2D1.SolidColorBrush> solidCache = new Dictionary<string, SharpDX.Direct2D1.SolidColorBrush>();
        static Dictionary<string, SharpDX.Direct2D1.LinearGradientBrush> linearBrushCache = new Dictionary<string, SharpDX.Direct2D1.LinearGradientBrush>();
        static Dictionary<string, SharpDX.Direct2D1.RadialGradientBrush> radialBrushCache = new Dictionary<string, SharpDX.Direct2D1.RadialGradientBrush>();

        #region Clear

        public static void ClearCache()
        {
            foreach (var item in bitmapCache)
            {
                SharpDX.Direct2D1.Bitmap b = item.Value;
                SharpDX.Utilities.Dispose(ref b);
            }
            bitmapCache.Clear();

            foreach (var item in pathCache)
            {
                var p = item.Value;
                SharpDX.Utilities.Dispose(ref p);
            }
            pathCache.Clear();

            foreach (var item in strokeCache)
            {
                var d = item.Value;
                SharpDX.Utilities.Dispose(ref d);
            }
            strokeCache.Clear();

            foreach (var item in geometryCache)
            {
                var d = item.Value;
                SharpDX.Utilities.Dispose(ref d);
            }
            geometryCache.Clear();

            foreach (var item in solidCache)
            {
                var d = item.Value;
                SharpDX.Utilities.Dispose(ref d);
            }
            solidCache.Clear();

            foreach (var item in linearBrushCache)
            {
                var d = item.Value;
                SharpDX.Utilities.Dispose(ref d);
            }
            linearBrushCache.Clear();

            foreach (var item in radialBrushCache)
            {
                var d = item.Value;
                SharpDX.Utilities.Dispose(ref d);
            }
            radialBrushCache.Clear();

            GC.Collect();
        }

        #endregion

        #region Bitmap

        public static SharpDX.Direct2D1.Bitmap GetBitmap(int id)
        {
            if (bitmapCache.ContainsKey(id))
                return bitmapCache[id];

            return null;
        }

        public static void AddBitmap(int id, SharpDX.Direct2D1.Bitmap bitmap)
        {
            bitmapCache.Add(id, bitmap);
        }

        public static void RemoveBitmap(int id)
        {
            if (bitmapCache.ContainsKey(id))
            {
                bitmapCache[id].Dispose();
                bitmapCache.Remove(id);
            }
                
        }

        #endregion

        #region Geometry

        public static SharpDX.Direct2D1.Geometry GetGeometry(string key)
        {
            if (geometryCache.ContainsKey(key))
            {
                return geometryCache[key];
            }

            return null;
        }

        public static void AddGeometry(string key, SharpDX.Direct2D1.Geometry g)
        {
            geometryCache.Add(key, g);
        }

        public static void RemoveGeometry(string key)
        {
            if (geometryCache.ContainsKey(key))
            {
                geometryCache[key].Dispose();
                geometryCache.Remove(key);
            }
                
        }

        #endregion

        #region Path

        public static SharpDX.Direct2D1.Geometry GetPath(int id)
        {
            if (pathCache.ContainsKey(id))
            {
                return pathCache[id];
            }

            return null;
        }

        public static void AddPath(int id, SharpDX.Direct2D1.Geometry g)
        {
            pathCache.Add(id, g);
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

        #region Stroke

        public static SharpDX.Direct2D1.StrokeStyle GetStroke(string key)
        {
            if (strokeCache.ContainsKey(key))
                return strokeCache[key];

            return null;
        }

        public static void AddStroke(string key, SharpDX.Direct2D1.StrokeStyle s)
        {
            strokeCache.Add(key, s);
        }

        public static void RemoveStroke(string key)
        {
            if (strokeCache.ContainsKey(key))
            {
                strokeCache[key].Dispose();
                strokeCache.Remove(key);
            }
        }

        #endregion

        #region SolidColor

        public static SharpDX.Direct2D1.SolidColorBrush GetSolidColor(string key)
        {
            if (solidCache.ContainsKey(key))
                return solidCache[key];

            return null;
        }

        public static void AddSolidColor(string key, SharpDX.Direct2D1.SolidColorBrush c)
        {
            solidCache.Add(key, c);
        }

        public static void RemoveSolidColor(string key)
        {
            if (solidCache.ContainsKey(key))
            {
                solidCache[key].Dispose();
                solidCache.Remove(key);
            }
        }

        #endregion

        #region LinearBrush

        public static SharpDX.Direct2D1.LinearGradientBrush GetLinearBrush(string key)
        {
            if (linearBrushCache.ContainsKey(key))
                return linearBrushCache[key];

            return null;
        }

        public static void AddLinearBrush(string key, SharpDX.Direct2D1.LinearGradientBrush l)
        {
            linearBrushCache.Add(key, l);
        }

        public static void RemoveLinearBrush(string key)
        {
            if (linearBrushCache.ContainsKey(key))
            {
                linearBrushCache[key].Dispose();
                linearBrushCache.Remove(key);
            }
        }

        #endregion

        #region RadialBrush

        public static SharpDX.Direct2D1.RadialGradientBrush GetRadialBrush(string key)
        {
            if (radialBrushCache.ContainsKey(key))
                return radialBrushCache[key];

            return null;
        }

        public static void AddRadialBrush(string key, SharpDX.Direct2D1.RadialGradientBrush rb)
        {
            radialBrushCache.Add(key, rb);
        }

        public static void RemoveRadialBrush(string key)
        {
            if (radialBrushCache.ContainsKey(key))
            {
                radialBrushCache[key].Dispose();
                radialBrushCache.Remove(key);
            }
        }

        #endregion
    }
}
