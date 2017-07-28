using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View
{
    internal class Draw
    {
        public Abstractions.ISlimDraw myDraw;

        public Draw(Abstractions.ISlimDraw iDraw)
        {
            myDraw = iDraw;
            iDraw.DrawUpdate += IDraw_DrawUpdate;
        }

        bool _updateView = false;
        public void UpdateView()
        {
            _updateView = true;
            myDraw.Restart();
        }

        int updateLoops = 0;
        int maxIdleUpdateLoops = 300;
        TimeSpan lastState = new TimeSpan();
        private void IDraw_DrawUpdate(object sender, Controls.EventTypes.DrawUpdateEventArgs e)
        {
            if (_updateView)
            {
                updateLoops = 0;
                _updateView = false;
            }

            var state = e.Time - lastState;
            Canvas.MyCanvas.RenderingTriger(e.Time, state);

            if (updateLoops < maxIdleUpdateLoops)
            {
                updateLoops++;
                DrawLoop();
            }
            else if (updateLoops == maxIdleUpdateLoops)
            {
                updateLoops = 0;
                //myDraw.Pause();
            }

            lastState = e.Time;
        }

        void DrawLoop()
        {
            myDraw.BeginDraw();

            DrawCanvas();
            DrawChildren();

            myDraw.EndDraw();
        }
        
        void DrawCanvas()
        {
            myDraw.DrawRect(Canvas.MyCanvas.BackroundRect, new Vector2(-Canvas.MyCanvas.Camera.X, -Canvas.MyCanvas.Camera.Y), Canvas.MyCanvas.Id);
        }

        void DrawChildren()
        {
            var items = Canvas.GlobalChildrenList.GetElementToDraw();
            foreach (var item in items)
            {
                DrawItem(item, new Vector2(-Canvas.MyCanvas.Camera.X, -Canvas.MyCanvas.Camera.Y));
            }
        }

        void DrawItem(Controls.UIElement basicElement, Vector2 parentPosition)
        {
            if (basicElement is Controls.Primitive.BasicPrimitive)
            {
                if (basicElement is Controls.Primitive.Ellipse)
                {
                    myDraw.DrawEllipse((basicElement as Controls.Primitive.Ellipse), parentPosition, basicElement.Id);
                }
                else if (basicElement is Controls.Primitive.Rectangle)
                {
                    myDraw.DrawRect((basicElement as Controls.Primitive.Rectangle), parentPosition, basicElement.Id);
                }
                else if (basicElement is Controls.Primitive.Line)
                {
                    myDraw.DrawLine((basicElement as Controls.Primitive.Line), parentPosition, basicElement.Id);
                }
                else if (basicElement is Controls.Primitive.Path)
                {
                    var p = (basicElement as Controls.Primitive.Path);
                    myDraw.DrawPath(p, parentPosition, p.pointsList, basicElement.Id);
                }
            }
            else if (basicElement is Controls.Image)
            {
                var i = basicElement as Controls.Image;
                
                myDraw.DrawImage(i, parentPosition, i.Id);
            }
            else if (basicElement is Controls.TextBlock)
            {
                var tb = basicElement as Controls.TextBlock;
                myDraw.DrawText(tb, parentPosition, tb.Id);
            }

            var pos = new Vector2(parentPosition.X + basicElement.ActualX, parentPosition.Y + basicElement.ActualY);
            foreach (var item in basicElement.Children)
            {
                DrawItem(item, pos);
            }
        }
    }
}
