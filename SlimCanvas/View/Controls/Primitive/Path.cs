using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    public class Path : BasicPrimitive
    {
        internal List<Vector2> pointsList;

        public Path()
        {
            pointsList = new List<Vector2>();
        }

        public void AddPoint(Vector2 point)
        {
            pointsList.Add(point);
            Canvas.ViewDraw.myDraw.RemoveFromPathCache(Id);
            SetSize();
        }

        public void AddPointRange(IEnumerable<Vector2> points)
        {
            pointsList.AddRange(points);
            Canvas.ViewDraw.myDraw.RemoveFromPathCache(Id);
            SetSize();
        }

        void SetSize()
        {
            var rect = GeometryHelper.GetSize(pointsList);
            Width = rect.Width;
            Height = rect.Height;
        }
    }
}
