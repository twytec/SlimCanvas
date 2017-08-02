using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    /// <summary>
    /// Only internal interfache
    /// </summary>
    public interface ISlimDraw
    {
        /// <summary>
        /// Only internal interfache
        /// </summary>
        void RemoveFromPathCache(int id);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void BeginDraw();

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void EndDraw();

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void Pause();

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void Restart();

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void Clear(Color color);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void DrawEllipse(View.Controls.Primitive.Ellipse ellipse, Vector2 position, int elementId);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void DrawRect(View.Controls.Primitive.Rectangle rect, Vector2 position, int elementId);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void DrawLine(View.Controls.Primitive.Line rect, Vector2 position, int elementId);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void DrawPath(View.Controls.Primitive.Path path, Vector2 position, List<Vector2> pointList, int elementId);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void DrawText(View.Controls.TextBlock textBlock, Vector2 position, int elementId);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        void DrawImage(View.Controls.Image img, Vector2 position, int elementId);

        /// <summary>
        /// Only internal interfache
        /// </summary>
        event SizeChangedEventHandler ViewSizeChanged;

        /// <summary>
        /// Only internal interfache
        /// </summary>
        event DrawUpdateEventHandler DrawUpdate;
    }
}
