using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.Droid
{
    internal class IUserInputDroid : Abstractions.IUserInput
    {
        float density;

        public IUserInputDroid(Android.Views.View view)
        {
            view.Touch += View_Touch;
            density = view.Resources.DisplayMetrics.Density;
        }

        #region Touch

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

        private void View_Touch(object sender, Android.Views.View.TouchEventArgs e)
        {
            PointerRoutedEventArgs args = new PointerRoutedEventArgs();
            
            for (int i = 0; i < e.Event.PointerCount; i++)
            {
                args.PointerId = e.Event.GetPointerId(i);

                var ptype = e.Event.GetToolType(i);
                if (ptype == MotionEventToolType.Mouse)
                    args.PointerType = PointerType.Mouse;
                else if (ptype == MotionEventToolType.Finger)
                    args.PointerType = PointerType.TouchPanel;
                else if (ptype == MotionEventToolType.Stylus)
                    args.PointerType = PointerType.Pen;

                args.X = e.Event.GetX(i);
                args.Y = e.Event.GetY(i);

                if (e.Event.Action == MotionEventActions.Move)
                {
                    args.GestureType = GestureType.None;
                    OnPointerMoved(args);

                    e.Handled = true;
                }

                if (e.Event.Action == MotionEventActions.Down)
                {
                    args.GestureType = GestureType.None;
                    OnPointerPressed(args);

                    e.Handled = true;
                }

                if (e.Event.Action == MotionEventActions.Up)
                {
                    args.GestureType = GestureType.None;
                    OnPointerReleased(args);

                    e.Handled = true;
                }
            }
        }
        
        #endregion
    }
}