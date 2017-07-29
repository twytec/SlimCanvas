using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.UWP
{
    internal class IUserInput : Abstractions.IUserInput
    {
        Windows.UI.Xaml.UIElement myElement;

        public IUserInput(Windows.UI.Xaml.UIElement element)
        {
            myElement = element;

            movedArgs = new PointerRoutedEventArgs();

            element.PointerMoved += Element_PointerMoved;
            element.PointerPressed += Element_PointerPressed;
            element.PointerReleased += Element_PointerReleased;
            //element.Tapped += Element_Tapped;
            element.RightTapped += Element_RightTapped;
            
        }
        
        #region Touch Mouse

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
        
        PointerRoutedEventArgs movedArgs;
        private void Element_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            movedArgs.GestureType = GestureType.None;

            GetPointerId(movedArgs, e);
            GetDefaultValues(movedArgs, e);

            OnPointerMoved(movedArgs);
        }
        
        private void Element_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            PointerRoutedEventArgs args = new PointerRoutedEventArgs()
            {
                GestureType = GestureType.None,
                PointerType = GetPointerType(e)
            };

            GetDefaultValues(args, e);

            PointerPressedTrigger(args);
        }

        private void Element_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            PointerRoutedEventArgs args = new PointerRoutedEventArgs()
            {
                GestureType = GestureType.None,
                PointerType = GetPointerType(e)
            };

            GetDefaultValues(args, e);

            PointerReleasedTrigger(args);
        }

        public event PointerRoutedEventHandler RightTapped;

        private void Element_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            PointerRoutedEventArgs args = new PointerRoutedEventArgs()
            {
                GestureType = GestureType.RightTapped,
                PointerType = GetPointerType(e)
            };

            GetDefaultValues(args, e);
            RightTapped?.Invoke(this, args);
        }

        #region Helpers

        void GetDefaultValues(PointerRoutedEventArgs args, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GetPointerId(args, e);
            GetPosition(args, e);
        }

        void GetPointerId(PointerRoutedEventArgs args, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (e is Windows.UI.Xaml.Input.PointerRoutedEventArgs)
            {
                var ee = e as Windows.UI.Xaml.Input.PointerRoutedEventArgs;
                args.PointerId = (int)ee.Pointer.PointerId;
            }
            else if (e is Windows.UI.Xaml.Input.TappedRoutedEventArgs)
            {
                var ee = e as Windows.UI.Xaml.Input.TappedRoutedEventArgs;
                args.PointerId = 0;
            }
        }

        void GetPosition(PointerRoutedEventArgs args, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (e is Windows.UI.Xaml.Input.PointerRoutedEventArgs)
            {
                var ee = e as Windows.UI.Xaml.Input.PointerRoutedEventArgs;

                var point = ee.GetCurrentPoint(myElement);
                args.X = (float)point.Position.X;
                args.Y = (float)point.Position.Y;
            }
            else if (e is Windows.UI.Xaml.Input.TappedRoutedEventArgs)
            {
                var ee = e as Windows.UI.Xaml.Input.TappedRoutedEventArgs;

                var point = ee.GetPosition(myElement);
                args.X = (float)point.X;
                args.Y = (float)point.Y;
            }
        }

        PointerType GetPointerType(Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (e is Windows.UI.Xaml.Input.PointerRoutedEventArgs)
            {
                var ee = e as Windows.UI.Xaml.Input.PointerRoutedEventArgs;

                if (ee.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
                    return PointerType.Mouse;
                else if (ee.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
                    return PointerType.TouchPanel;
                else if (ee.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
                    return PointerType.Pen;
            }
            else if (e is Windows.UI.Xaml.Input.TappedRoutedEventArgs)
            {
                var ee = e as Windows.UI.Xaml.Input.TappedRoutedEventArgs;

                if (ee.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
                    return PointerType.Mouse;
                else if (ee.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch)
                    return PointerType.TouchPanel;
                else if (ee.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Pen)
                    return PointerType.Pen;
            }
            
            return PointerType.None;
        }

        #endregion

        #endregion
    }
}
