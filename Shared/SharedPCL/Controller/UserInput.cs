using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.Controller
{
    internal class UserInput
    {
        Abstractions.IUserInput input;

        public UserInput(Abstractions.IUserInput input)
        {
            this.input = input;
            
            input.PointerMoved += Input_PointerMoved;
            input.PointerPressed += Input_PointerPressed;
            input.PointerReleased += Input_PointerReleased;

            //Only with Mouse
            input.RightTapped += Input_RightTapped;

            //input.KeyDown += Input_KeyDown;
            //input.KeyUp += Input_KeyUp;
        }
        
        #region Keyboard

        //private void Input_KeyUp(object sender, KeyRoutedEventArgs e)
        //{
        //    KeyTrigger(false, e);
        //}

        //private void Input_KeyDown(object sender, KeyRoutedEventArgs e)
        //{
        //    KeyTrigger(true, e);
        //}

        //void KeyTrigger(bool keyDown, KeyRoutedEventArgs e)
        //{
        //    var child = Canvas.GlobalChildrenList.GetAll().OrderByDescending(i => i.Id).OrderBy(z => z.ZIndex);

        //    if (keyDown)
        //    {
        //        foreach (var item in child)
        //            item.KeyDownTrigger(e);
        //    }
        //    else
        //    {
        //        foreach (var item in child)
        //            item.KeyUpTrigger(e);
        //    }
        //}

        #endregion

        #region Mouse Touch

        Dictionary<int, TouchState> stateList = new Dictionary<int, TouchState>();
        int elapseTimeForSwipe = 300;
        int elapseTimeForRightTapped = 500;
        
        private void Input_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            TriggerMouseTouch(e, TriggerMouseTouchEvents.Released);
        }

        private void Input_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            TriggerMouseTouch(e, TriggerMouseTouchEvents.Pressed);
        }

        private void Input_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            TriggerMouseTouch(e, TriggerMouseTouchEvents.Moved);
        }

        private void Input_RightTapped(object sender, PointerRoutedEventArgs e)
        {
            TriggerMouseTouch(e, TriggerMouseTouchEvents.RightTapped);
        }

        void TriggerMouseTouch(PointerRoutedEventArgs e, TriggerMouseTouchEvents t)
        {
            var items = Canvas.GlobalChildrenList.ElementsUnderPointXY(e);

            if (t == TriggerMouseTouchEvents.RightTapped)
            {
                foreach (var item in items)
                {
                    e.GestureType = GestureType.RightTapped;
                    item.RightTappedTrigger(e);
                }

                return;
            }

            foreach (var item in items)
            {
                if (t == TriggerMouseTouchEvents.Pressed)
                {
                    item.PointerPressedTrigger(e);

                    if (GetState(e.PointerId) == null)
                    {
                        var state = new TouchState()
                        {
                            Args = e,
                            PressedTimestamp = DateTime.Now
                        };
                        stateList.Add(e.PointerId, state);
                    }
                }
                else if (t == TriggerMouseTouchEvents.Moved)
                {
                    if (GetState(e.PointerId) is TouchState state)
                    {
                        item.PointerMovedTrigger(e);
                        state.Moved = true;
                    }
                    else
                    {
                        //If moved with Mouse
                        item.PointerMovedTrigger(e);
                    }
                }
                else if (t == TriggerMouseTouchEvents.Released)
                {
                    if (GetState(e.PointerId) is TouchState state)
                    {
                        item.PointerReleasedTrigger(e);
                        var elapseTime = DateTime.Now - state.PressedTimestamp;

                        if (state.Moved)
                        {
                            if (elapseTime.TotalMilliseconds <= elapseTimeForSwipe)
                            {
                                #region Swipe

                                var sX = state.Args.X - e.X;
                                var sY = state.Args.Y - e.Y;

                                if (sX < -50)
                                {
                                    e.GestureType = GestureType.SwipeRight;
                                    item.SwipeRightTrigger(e);
                                }
                                else if (sX > 50)
                                {
                                    e.GestureType = GestureType.SwipeLeft;
                                    item.SwipeLeftTrigger(e);
                                }

                                if (sY < -50)
                                {
                                    e.GestureType = GestureType.SwipeBottom;
                                    item.SwipeBottomTrigger(e);
                                }
                                else if (sY > 50)
                                {
                                    e.GestureType = GestureType.SwipeTop;
                                    item.SwipeTopTrigger(e);
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            #region Tapped RightTapped
                            
                            if (elapseTime.TotalMilliseconds <= elapseTimeForRightTapped)
                            {
                                e.GestureType = GestureType.Tapped;
                                item.TappedTrigger(e);
                            }
                            else
                            {
                                e.GestureType = GestureType.RightTapped;
                                item.RightTappedTrigger(e);
                            }

                            #endregion
                        }

                        if (item == items.Last())
                        {
                            stateList.Remove(e.PointerId);
                        }
                    }
                }
                
                if (e.Handle)
                    break;
            }

            items = null;
        }

        TouchState GetState(int id)
        {
            if (stateList.ContainsKey(id))
            {
                return stateList[id];
            }

            return null;
        }

        enum TriggerMouseTouchEvents
        {
            Moved,
            Pressed,
            Released,
            RightTapped
        }

        class TouchState
        {
            public PointerRoutedEventArgs Args { get; set; }
            public bool Moved { get; set; }
            public DateTime PressedTimestamp { get; set; }
        }

        #endregion
    }
}
