using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Abstractions
{
    public interface ISlimDraw
    {
        void RemoveFromPathCache(int id);
        
        void BeginDraw();
        void EndDraw();

        void Pause();
        void Restart();

        void Clear(Color color);

        void DrawEllipse(View.Controls.Primitive.Ellipse ellipse, Vector2 position, int elementId);
        void DrawRect(View.Controls.Primitive.Rectangle rect, Vector2 position, int elementId);
        void DrawLine(View.Controls.Primitive.Line rect, Vector2 position, int elementId);
        void DrawPath(View.Controls.Primitive.Path path, Vector2 position, List<Vector2> pointList, int elementId);

        void DrawText(View.Controls.TextBlock textBlock, Vector2 position, int elementId);
        void DrawImage(View.Controls.Image img, Vector2 position, int elementId);

        event SizeChangedEventHandler ViewSizeChanged;

        event DrawUpdateEventHandler DrawUpdate;
    }
}
