using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using SlimCanvas.View.Controls.EventTypes;
using SlimCanvas.View.Controls.Primitive;
using SlimCanvas.View.Controls;
using CoreText;

namespace SlimCanvas.iOS
{
    public class SlimCanvasIOS : UIView, Abstractions.ISlimDraw, Abstractions.IUserInput
    {
        public Canvas SlimCanvasPCL;

        UIImageView imageView;
        DrawLayer ctx = null;
        CGSize canSize;

        CADisplayLink displayLink;
        bool animating;

        public SlimCanvasIOS()
        {
            UserInteractionEnabled = true;
            MultipleTouchEnabled = true;

            SlimCanvasPCL = new Canvas(this, this, new IAssetsiOS(), new IGraphicsiOS())
            {
                Plattform = Plattform.IOS
            };
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.BackgroundColor = UIColor.Blue;

            imageView = new UIImageView(new CGRect(0,0, Frame.Width, Frame.Height));
            this.AddSubview(imageView);
        }

        #region UserInput

        #region PointerPressed

        public event PointerRoutedEventHandler PointerPressed;
        protected virtual void OnPointerPressed(PointerRoutedEventArgs e)
        {
            PointerPressed?.Invoke(this, e);
        }
        internal void PointerPressedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerPressed(e);
        }

        #endregion

        #region PointerMoved

        public event PointerRoutedEventHandler PointerMoved;
        protected virtual void OnPointerMoved(PointerRoutedEventArgs e)
        {
            PointerMoved?.Invoke(this, e);
        }
        internal void PointerMovedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerMoved(e);
        }

        #endregion

        #region PointerReleased

        public event PointerRoutedEventHandler PointerReleased;
        protected virtual void OnPointerReleased(PointerRoutedEventArgs e)
        {
            PointerReleased?.Invoke(this, e);
        }
        internal void PointerReleasedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerReleased(e);
        }

        #endregion

        public event PointerRoutedEventHandler RightTapped;
        protected virtual void OnRightTapped(PointerRoutedEventArgs e)
        {
            RightTapped?.Invoke(this, e);
        }

        PointerRoutedEventArgs pressedArgs;
        
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            pressedArgs = new PointerRoutedEventArgs()
            {
                PointerType = PointerType.TouchPanel,
                GestureType = GestureType.None
            };

            foreach (var item in touches.Cast<UITouch>())
            {
                var point = item.LocationInView(this);
                pressedArgs.X = point.X;
                pressedArgs.Y = point.Y;

                var id = item.Handle.ToInt32();
                pressedArgs.PointerId = id;
                PointerPressedTrigger(pressedArgs);
            }
        }

        PointerRoutedEventArgs movedArgs;
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            movedArgs = new PointerRoutedEventArgs()
            {
                PointerType = PointerType.TouchPanel,
                GestureType = GestureType.None
            };

            foreach (var item in touches.Cast<UITouch>())
            {
                var point = item.LocationInView(this);
                movedArgs.X = point.X;
                movedArgs.Y = point.Y;

                var id = item.Handle.ToInt32();
                movedArgs.PointerId = id;
                PointerMovedTrigger(movedArgs);
            }
        }

        PointerRoutedEventArgs releasedArgs;
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            releasedArgs = new PointerRoutedEventArgs()
            {
                PointerType = PointerType.TouchPanel,
                GestureType = GestureType.None
            };

            foreach (var item in touches.Cast<UITouch>())
            {
                var point = item.LocationInView(this);
                releasedArgs.X = point.X;
                releasedArgs.Y = point.Y;

                var id = item.Handle.ToInt32();
                releasedArgs.PointerId = id;
                PointerReleasedTrigger(releasedArgs);
            }
        }

        #endregion

        #region Event

        #region Size changed

        public override CGRect Frame { get => base.Frame; set { base.Frame = value; ViewSizeChangedTrigger((int)value.Width, (int)value.Height); } }
        public override CGRect Bounds { get => base.Bounds; set { base.Bounds = value; ViewSizeChangedTrigger((int)value.Width, (int)value.Height); } }

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