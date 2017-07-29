using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.iOS
{
    internal class IUserInput : Abstractions.IUserInput
    {
        UIView uiView;

        public IUserInput(UIView uiView)
        {
            this.uiView = uiView;
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

        public void TouchesBegan(NSSet touches, UIEvent evt)
        {
            pressedArgs = new PointerRoutedEventArgs()
            {
                PointerType = PointerType.TouchPanel,
                GestureType = GestureType.None
            };

            foreach (var item in touches.Cast<UITouch>())
            {
                var point = item.LocationInView(uiView);
                pressedArgs.X = point.X;
                pressedArgs.Y = point.Y;

                var id = item.Handle.ToInt32();
                pressedArgs.PointerId = id;
                PointerPressedTrigger(pressedArgs);
            }
        }

        PointerRoutedEventArgs movedArgs;
        public void TouchesMoved(NSSet touches, UIEvent evt)
        {
            movedArgs = new PointerRoutedEventArgs()
            {
                PointerType = PointerType.TouchPanel,
                GestureType = GestureType.None
            };

            foreach (var item in touches.Cast<UITouch>())
            {
                var point = item.LocationInView(uiView);
                movedArgs.X = point.X;
                movedArgs.Y = point.Y;

                var id = item.Handle.ToInt32();
                movedArgs.PointerId = id;
                PointerMovedTrigger(movedArgs);
            }
        }

        PointerRoutedEventArgs releasedArgs;
        public void TouchesEnded(NSSet touches, UIEvent evt)
        {
            releasedArgs = new PointerRoutedEventArgs()
            {
                PointerType = PointerType.TouchPanel,
                GestureType = GestureType.None
            };

            foreach (var item in touches.Cast<UITouch>())
            {
                var point = item.LocationInView(uiView);
                releasedArgs.X = point.X;
                releasedArgs.Y = point.Y;

                var id = item.Handle.ToInt32();
                releasedArgs.PointerId = id;
                PointerReleasedTrigger(releasedArgs);
            }
        }

        #endregion

    }
}