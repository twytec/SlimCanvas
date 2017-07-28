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

            //Windows.UI.Xaml.Window.Current.Content.KeyDown += Content_KeyDown;
            //Windows.UI.Xaml.Window.Current.Content.KeyUp += Content_KeyUp;
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

        #region Keyboard
        
        private void Content_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            KeyRoutedEventArgs args = new KeyRoutedEventArgs()
            {
                Key = GetKey(e.Key),
                RepeatCount = e.KeyStatus.RepeatCount,
                ScanCode = e.KeyStatus.ScanCode,
                DeviceId = e.DeviceId
            };
            KeyUpTrigger(args);
        }

        public event KeyRoutedEventHandler KeyUp;
        protected virtual void OnKeyUp(KeyRoutedEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }
        internal void KeyUpTrigger(KeyRoutedEventArgs e)
        {
            OnKeyUp(e);
        }


        private void Content_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var k = GetKey(e.Key);

            if (k == Keys.None)
                k = (Keys)Enum.Parse(typeof(Keys), e.Key.ToString());
            
            KeyRoutedEventArgs args = new KeyRoutedEventArgs()
            {
                Key = k,
                RepeatCount = e.KeyStatus.RepeatCount,
                ScanCode = e.KeyStatus.ScanCode,
                DeviceId = e.DeviceId
            };
            KeyDownTrigger(args);
        }
        public event KeyRoutedEventHandler KeyDown;
        protected virtual void OnKeyDown(KeyRoutedEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }
        internal void KeyDownTrigger(KeyRoutedEventArgs e)
        {
            OnKeyDown(e);
        }

        #region Helper

        Keys GetKey(Windows.System.VirtualKey wKey)
        {
            switch (wKey)
            {
                case Windows.System.VirtualKey.None:
                    return Keys.None;
                case Windows.System.VirtualKey.LeftButton:
                    return Keys.LeftButton;
                case Windows.System.VirtualKey.RightButton:
                    return Keys.RightButton;
                case Windows.System.VirtualKey.Cancel:
                    return Keys.Cancel;
                case Windows.System.VirtualKey.MiddleButton:
                    return Keys.MiddleButton;
                case Windows.System.VirtualKey.XButton1:
                    return Keys.XButton1;
                case Windows.System.VirtualKey.XButton2:
                    return Keys.XButton2;
                case Windows.System.VirtualKey.Back:
                    return Keys.Back;
                case Windows.System.VirtualKey.Tab:
                    return Keys.Tab;
                case Windows.System.VirtualKey.Clear:
                    return Keys.Clear;
                case Windows.System.VirtualKey.Enter:
                    return Keys.Enter;
                case Windows.System.VirtualKey.Shift:
                    return Keys.Shift;
                case Windows.System.VirtualKey.Control:
                    return Keys.Control;
                case Windows.System.VirtualKey.Menu:
                    return Keys.Menu;
                case Windows.System.VirtualKey.Pause:
                    return Keys.Pause;
                case Windows.System.VirtualKey.CapitalLock:
                    return Keys.CapitalLock;
                case Windows.System.VirtualKey.Kana:
                    return Keys.Kana;
                case Windows.System.VirtualKey.Junja:
                    return Keys.Junja;
                case Windows.System.VirtualKey.Final:
                    return Keys.Final;
                case Windows.System.VirtualKey.Hanja:
                    return Keys.Hanja;
                case Windows.System.VirtualKey.Escape:
                    return Keys.Escape;
                case Windows.System.VirtualKey.Convert:
                    return Keys.Convert;
                case Windows.System.VirtualKey.NonConvert:
                    return Keys.NonConvert;
                case Windows.System.VirtualKey.Accept:
                    return Keys.Accept;
                case Windows.System.VirtualKey.ModeChange:
                    return Keys.ModeChange;
                case Windows.System.VirtualKey.Space:
                    return Keys.Space;
                case Windows.System.VirtualKey.PageUp:
                    return Keys.PageUp;
                case Windows.System.VirtualKey.PageDown:
                    return Keys.PageDown;
                case Windows.System.VirtualKey.End:
                    return Keys.End;
                case Windows.System.VirtualKey.Home:
                    return Keys.Home;
                case Windows.System.VirtualKey.Left:
                    return Keys.Left;
                case Windows.System.VirtualKey.Up:
                    return Keys.Up;
                case Windows.System.VirtualKey.Right:
                    return Keys.Right;
                case Windows.System.VirtualKey.Down:
                    return Keys.Down;
                case Windows.System.VirtualKey.Select:
                    return Keys.Select;
                case Windows.System.VirtualKey.Print:
                    return Keys.Print;
                case Windows.System.VirtualKey.Execute:
                    return Keys.Execute;
                case Windows.System.VirtualKey.Snapshot:
                    return Keys.Snapshot;
                case Windows.System.VirtualKey.Insert:
                    return Keys.Insert;
                case Windows.System.VirtualKey.Delete:
                    return Keys.Delete;
                case Windows.System.VirtualKey.Help:
                    return Keys.Help;
                case Windows.System.VirtualKey.Number0:
                    return Keys.Number0;
                case Windows.System.VirtualKey.Number1:
                    return Keys.Number1;
                case Windows.System.VirtualKey.Number2:
                    return Keys.Number2;
                case Windows.System.VirtualKey.Number3:
                    return Keys.Number3;
                case Windows.System.VirtualKey.Number4:
                    return Keys.Number4;
                case Windows.System.VirtualKey.Number5:
                    return Keys.Number5;
                case Windows.System.VirtualKey.Number6:
                    return Keys.Number6;
                case Windows.System.VirtualKey.Number7:
                    return Keys.Number7;
                case Windows.System.VirtualKey.Number8:
                    return Keys.Number8;
                case Windows.System.VirtualKey.Number9:
                    return Keys.Number9;
                case Windows.System.VirtualKey.A:
                    return Keys.A;
                case Windows.System.VirtualKey.B:
                    return Keys.B;
                case Windows.System.VirtualKey.C:
                    return Keys.C;
                case Windows.System.VirtualKey.D:
                    return Keys.D;
                case Windows.System.VirtualKey.E:
                    return Keys.E;
                case Windows.System.VirtualKey.F:
                    return Keys.F;
                case Windows.System.VirtualKey.G:
                    return Keys.G;
                case Windows.System.VirtualKey.H:
                    return Keys.H;
                case Windows.System.VirtualKey.I:
                    return Keys.I;
                case Windows.System.VirtualKey.J:
                    return Keys.J;
                case Windows.System.VirtualKey.K:
                    return Keys.K;
                case Windows.System.VirtualKey.L:
                    return Keys.L;
                case Windows.System.VirtualKey.M:
                    return Keys.M;
                case Windows.System.VirtualKey.N:
                    return Keys.N;
                case Windows.System.VirtualKey.O:
                    return Keys.O;
                case Windows.System.VirtualKey.P:
                    return Keys.P;
                case Windows.System.VirtualKey.Q:
                    return Keys.Q;
                case Windows.System.VirtualKey.R:
                    return Keys.R;
                case Windows.System.VirtualKey.S:
                    return Keys.S;
                case Windows.System.VirtualKey.T:
                    return Keys.T;
                case Windows.System.VirtualKey.U:
                    return Keys.U;
                case Windows.System.VirtualKey.V:
                    return Keys.V;
                case Windows.System.VirtualKey.W:
                    return Keys.W;
                case Windows.System.VirtualKey.X:
                    return Keys.X;
                case Windows.System.VirtualKey.Y:
                    return Keys.Y;
                case Windows.System.VirtualKey.Z:
                    return Keys.Z;
                case Windows.System.VirtualKey.LeftWindows:
                    return Keys.LeftWindows;
                case Windows.System.VirtualKey.RightWindows:
                    return Keys.RightWindows;
                case Windows.System.VirtualKey.Application:
                    return Keys.Application;
                case Windows.System.VirtualKey.Sleep:
                    return Keys.Sleep;
                case Windows.System.VirtualKey.NumberPad0:
                    return Keys.NumberPad0;
                case Windows.System.VirtualKey.NumberPad1:
                    return Keys.NumberPad1;
                case Windows.System.VirtualKey.NumberPad2:
                    return Keys.NumberPad2;
                case Windows.System.VirtualKey.NumberPad3:
                    return Keys.NumberPad3;
                case Windows.System.VirtualKey.NumberPad4:
                    return Keys.NumberPad4;
                case Windows.System.VirtualKey.NumberPad5:
                    return Keys.NumberPad5;
                case Windows.System.VirtualKey.NumberPad6:
                    return Keys.NumberPad6;
                case Windows.System.VirtualKey.NumberPad7:
                    return Keys.NumberPad7;
                case Windows.System.VirtualKey.NumberPad8:
                    return Keys.NumberPad8;
                case Windows.System.VirtualKey.NumberPad9:
                    return Keys.NumberPad9;
                case Windows.System.VirtualKey.Multiply:
                    return Keys.Multiply;
                case Windows.System.VirtualKey.Add:
                    return Keys.Add;
                case Windows.System.VirtualKey.Separator:
                    return Keys.Separator;
                case Windows.System.VirtualKey.Subtract:
                    return Keys.Subtract;
                case Windows.System.VirtualKey.Decimal:
                    return Keys.Decimal;
                case Windows.System.VirtualKey.Divide:
                    return Keys.Divide;
                case Windows.System.VirtualKey.F1:
                    return Keys.F1;
                case Windows.System.VirtualKey.F2:
                    return Keys.F2;
                case Windows.System.VirtualKey.F3:
                    return Keys.F3;
                case Windows.System.VirtualKey.F4:
                    return Keys.F4;
                case Windows.System.VirtualKey.F5:
                    return Keys.F5;
                case Windows.System.VirtualKey.F6:
                    return Keys.F6;
                case Windows.System.VirtualKey.F7:
                    return Keys.F7;
                case Windows.System.VirtualKey.F8:
                    return Keys.F8;
                case Windows.System.VirtualKey.F9:
                    return Keys.F9;
                case Windows.System.VirtualKey.F10:
                    return Keys.F10;
                case Windows.System.VirtualKey.F11:
                    return Keys.F11;
                case Windows.System.VirtualKey.F12:
                    return Keys.F12;
                case Windows.System.VirtualKey.F13:
                    return Keys.F13;
                case Windows.System.VirtualKey.F14:
                    return Keys.F14;
                case Windows.System.VirtualKey.F15:
                    return Keys.F15;
                case Windows.System.VirtualKey.F16:
                    return Keys.F16;
                case Windows.System.VirtualKey.F17:
                    return Keys.F17;
                case Windows.System.VirtualKey.F18:
                    return Keys.F18;
                case Windows.System.VirtualKey.F19:
                    return Keys.F19;
                case Windows.System.VirtualKey.F20:
                    return Keys.F20;
                case Windows.System.VirtualKey.F21:
                    return Keys.F21;
                case Windows.System.VirtualKey.F22:
                    return Keys.F22;
                case Windows.System.VirtualKey.F23:
                    return Keys.F23;
                case Windows.System.VirtualKey.F24:
                    return Keys.F24;
                case Windows.System.VirtualKey.NavigationView:
                    return Keys.NavigationView;
                case Windows.System.VirtualKey.NavigationMenu:
                    return Keys.NavigationMenu;
                case Windows.System.VirtualKey.NavigationUp:
                    return Keys.NavigationUp;
                case Windows.System.VirtualKey.NavigationDown:
                    return Keys.NavigationDown;
                case Windows.System.VirtualKey.NavigationLeft:
                    return Keys.NavigationLeft;
                case Windows.System.VirtualKey.NavigationRight:
                    return Keys.NavigationRight;
                case Windows.System.VirtualKey.NavigationAccept:
                    return Keys.NavigationAccept;
                case Windows.System.VirtualKey.NavigationCancel:
                    return Keys.NavigationCancel;
                case Windows.System.VirtualKey.NumberKeyLock:
                    return Keys.NumberKeyLock;
                case Windows.System.VirtualKey.Scroll:
                    return Keys.Scroll;
                case Windows.System.VirtualKey.LeftShift:
                    return Keys.LeftShift;
                case Windows.System.VirtualKey.RightShift:
                    return Keys.RightShift;
                case Windows.System.VirtualKey.LeftControl:
                    return Keys.LeftControl;
                case Windows.System.VirtualKey.RightControl:
                    return Keys.RightControl;
                case Windows.System.VirtualKey.LeftMenu:
                    return Keys.LeftMenu;
                case Windows.System.VirtualKey.RightMenu:
                    return Keys.RightMenu;
                case Windows.System.VirtualKey.GoBack:
                    return Keys.GoBack;
                case Windows.System.VirtualKey.GoForward:
                    return Keys.GoForward;
                case Windows.System.VirtualKey.Refresh:
                    return Keys.Refresh;
                case Windows.System.VirtualKey.Stop:
                    return Keys.Stop;
                case Windows.System.VirtualKey.Search:
                    return Keys.Search;
                case Windows.System.VirtualKey.Favorites:
                    return Keys.Favorites;
                case Windows.System.VirtualKey.GoHome:
                    return Keys.GoHome;
                case Windows.System.VirtualKey.GamepadA:
                    return Keys.GamepadA;
                case Windows.System.VirtualKey.GamepadB:
                    return Keys.GamepadB;
                case Windows.System.VirtualKey.GamepadX:
                    return Keys.GamepadX;
                case Windows.System.VirtualKey.GamepadY:
                    return Keys.GamepadY;
                case Windows.System.VirtualKey.GamepadRightShoulder:
                    return Keys.GamepadRightShoulder;
                case Windows.System.VirtualKey.GamepadLeftShoulder:
                    return Keys.GamepadLeftShoulder;
                case Windows.System.VirtualKey.GamepadLeftTrigger:
                    return Keys.GamepadLeftTrigger;
                case Windows.System.VirtualKey.GamepadRightTrigger:
                    return Keys.GamepadRightTrigger;
                case Windows.System.VirtualKey.GamepadDPadUp:
                    return Keys.GamepadDPadUp;
                case Windows.System.VirtualKey.GamepadDPadDown:
                    return Keys.GamepadDPadDown;
                case Windows.System.VirtualKey.GamepadDPadLeft:
                    return Keys.GamepadDPadLeft;
                case Windows.System.VirtualKey.GamepadDPadRight:
                    return Keys.GamepadDPadRight;
                case Windows.System.VirtualKey.GamepadMenu:
                    return Keys.GamepadMenu;
                case Windows.System.VirtualKey.GamepadView:
                    return Keys.GamepadView;
                case Windows.System.VirtualKey.GamepadLeftThumbstickButton:
                    return Keys.GamepadLeftThumbstickButton;
                case Windows.System.VirtualKey.GamepadRightThumbstickButton:
                    return Keys.GamepadRightThumbstickButton;
                case Windows.System.VirtualKey.GamepadLeftThumbstickUp:
                    return Keys.GamepadLeftThumbstickUp;
                case Windows.System.VirtualKey.GamepadLeftThumbstickDown:
                    return Keys.GamepadLeftThumbstickDown;
                case Windows.System.VirtualKey.GamepadLeftThumbstickRight:
                    return Keys.GamepadLeftThumbstickRight;
                case Windows.System.VirtualKey.GamepadLeftThumbstickLeft:
                    return Keys.GamepadLeftThumbstickLeft;
                case Windows.System.VirtualKey.GamepadRightThumbstickUp:
                    return Keys.GamepadRightThumbstickUp;
                case Windows.System.VirtualKey.GamepadRightThumbstickDown:
                    return Keys.GamepadRightThumbstickDown;
                case Windows.System.VirtualKey.GamepadRightThumbstickRight:
                    return Keys.GamepadRightThumbstickRight;
                case Windows.System.VirtualKey.GamepadRightThumbstickLeft:
                    return Keys.GamepadRightThumbstickLeft;
                default:
                    return Keys.None;
            }
        }

        #endregion

        #endregion
    }
}
