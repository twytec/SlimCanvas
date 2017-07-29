using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SlimCanvas.View.Controls.EventTypes;
using CoreAnimation;
using CoreGraphics;
using SlimCanvas.View.Controls;
using SlimCanvas.View.Controls.Primitive;

namespace SlimCanvas.iOS
{
    internal class DrawInCanvas : Abstractions.ISlimDraw
    {
        public UIImageView imageView;
        DrawLayer ctx = null;
        CGSize canSize;

        CADisplayLink displayLink;
        bool animating;

        public DrawInCanvas()
        {

        }

        #region Event

        #region SizeChanged

        public event SizeChangedEventHandler ViewSizeChanged;
        protected virtual void OnViewSizeChanged(SizeChangedEventArgs e)
        {
            ViewSizeChanged?.Invoke(this, e);
        }

        SizeChangedEventArgs sizeArgs;
        internal void ViewSizeChangedTrigger(int width, int height)
        {
            if (sizeArgs == null)
                sizeArgs = new SizeChangedEventArgs();

            if (sizeArgs.NewWidth != width || sizeArgs.NewHeight != height)
            {
                sizeArgs.NewWidth = width;
                sizeArgs.NewHeight = height;
                OnViewSizeChanged(sizeArgs);
            }
        }

        #endregion

        #region Update

        System.Diagnostics.Stopwatch stopwatch;
        public void Pause()
        {
            if (animating)
            {
                displayLink.Invalidate();
                displayLink = null;

                animating = false;
                stopwatch.Stop();
            }
        }

        public void Restart()
        {
            if (!animating)
            {
                displayLink = CADisplayLink.Create(Update);
                displayLink.FrameInterval = 1; //1 = FPS 60; 2 = FPS 30;
                displayLink.AddToRunLoop(NSRunLoop.Current, NSRunLoop.NSDefaultRunLoopMode);

                animating = true;

                if (stopwatch == null)
                    stopwatch = new System.Diagnostics.Stopwatch();

                stopwatch.Restart();
            }
        }

        public event DrawUpdateEventHandler DrawUpdate;
        protected virtual void OnDrawUpdate(DrawUpdateEventArgs e)
        {
            DrawUpdate?.Invoke(this, e);
        }

        void Update()
        {
            DrawUpdateEventArgs e = new DrawUpdateEventArgs()
            {
                Time = stopwatch.Elapsed
            };
            OnDrawUpdate(e);
        }

        #endregion

        #endregion

        #region BeginDraw EndDraw

        public void BeginDraw()
        {
            if (ctx != null)
                return;

            try
            {
                canSize = new CGSize(imageView.Frame.Size);

                ctx = new DrawLayer((int)canSize.Width, (int)canSize.Height);
            }
            catch (Exception)
            {
                ctx = null;
                throw;
            }
        }

        public void EndDraw()
        {
            imageView.Image = UIImage.FromImage(ctx.GetAsImage());
            ctx.Dispose();
            ctx = null;
        }

        #endregion

        #region CanDraw

        bool CanDraw()
        {
            if (ctx == null)
                return false;

            return true;
        }

        #endregion

        #region Clear

        public void Clear(Color color)
        {
            if (!CanDraw())
                return;

            ctx.Clear(color);
        }

        #endregion

        #region Draw Primitive

        #region DrawEllipse

        public void DrawEllipse(Ellipse ellipse, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            ctx.DrawEllipse(ellipse, parentPosition, elementId);
        }

        #endregion

        #region DrawRect

        public void DrawRect(Rectangle rectangle, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            ctx.DrawRect(rectangle, parentPosition, elementId);
        }

        #endregion

        #region DrawLine

        public void DrawLine(Line line, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            ctx.DrawLine(line, parentPosition, elementId);
        }

        #endregion

        #region DrawPath

        public void DrawPath(Path path, Vector2 parentPosition, List<Vector2> pointList, int elementId)
        {
            if (!CanDraw())
                return;

            ctx.DrawPath(path, pointList, parentPosition, elementId);
        }

        #endregion

        #endregion

        #region DrawText

        public void DrawText(TextBlock tb, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            var tbSize = ctx.DrawText(tb, parentPosition, elementId);

            if (tbSize.Width != tb.Width || tbSize.Height != tb.Height)
            {
                tb.Width = tbSize.Width;
                tb.Height = tbSize.Height;
            }
        }

        #endregion

        #region DrawImage

        public void DrawImage(Image img, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            var bitmap = img.Source as IBitmapiOS;

            ctx.DrawImage(img, parentPosition, elementId);
        }

        #endregion

        public void RemoveFromPathCache(int id)
        {
            LocalCache.RemovePath(id);
        }
    }
}