using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    internal class GeometryHelper
    {
        public static Rect GetSize(Vector2 startPoint, Vector2 endPoint)
        {
            var minX = Math.Min(startPoint.X, endPoint.X);
            var maxX = Math.Max(startPoint.X, endPoint.X);

            var minY = Math.Min(startPoint.Y, endPoint.Y);
            var maxY = Math.Max(startPoint.Y, endPoint.Y);

            return new Rect((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY));
        }

        public static Rect GetSize(IEnumerable<Vector2> points)
        {
            var ordedX = points.OrderBy(x => x.X).ToList();
            var minX = ordedX.First();
            var maxX = ordedX.Last();

            var ordedY = points.OrderBy(y => y.Y);
            var minY = ordedY.First();
            var maxY = ordedY.Last();

            var rect = new Rect()
            {
                X = (int)minX.X,
                Y = (int)minY.Y,
                Width = (int)(maxX.X - minX.X),
                Height = (int)(maxY.Y - minY.Y)
            };

            return rect;
        }
    }
}
