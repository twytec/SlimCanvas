using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Primitive
{
    /// <summary>
    /// to be added
    /// </summary>
    public class Path : BasicPrimitive
    {
        internal List<Vector2> pointsList;

        /// <summary>
        /// to be added
        /// </summary>
        public Path()
        {
            pointsList = new List<Vector2>();
        }

        /// <summary>
        /// to be added
        /// </summary>
        public void AddPoint(Vector2 point)
        {
            pointsList.Add(point);
            Canvas.ViewDraw.myDraw.RemoveFromPathCache(Id);
            SetSize();
        }

        /// <summary>
        /// to be added
        /// </summary>
        public void AddPointRange(IEnumerable<Vector2> points)
        {
            pointsList.AddRange(points);
            Canvas.ViewDraw.myDraw.RemoveFromPathCache(Id);
            SetSize();
        }

        /// <summary>
        /// to be added
        /// </summary>
        void SetSize()
        {
            var rect = GeometryHelper.GetSize(pointsList);
            Width = rect.Width;
            Height = rect.Height;
        }
    }
}
