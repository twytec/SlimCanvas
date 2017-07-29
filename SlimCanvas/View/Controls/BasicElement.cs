using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls
{
    /// <summary>
    /// BasicElement
    /// </summary>
    public class BasicElement
    {
        /// <summary>
        /// BasicElement
        /// </summary>
        public BasicElement()
        {
            _HeightProperty = new Propertys.HeightProperty(this);
            _WidthProperty = new Propertys.WidthProperty(this);

            if (Canvas.GlobalChildrenList == null)
                Canvas.GlobalChildrenList = new Collections.GlobalChildrenList();

            Id = Canvas.GlobalChildrenList.GetNextElementId();
            
            if (this is Canvas)
            {
                ParentId = -2;
                Id = -1;
            }

            Children = new Collections.ChildrenCollection(Id, this);
        }

        /// <summary>
        /// HeightProperty
        /// </summary>
        public Propertys.HeightProperty HeightProperty { get { return _HeightProperty; } }

        /// <summary>
        /// WidthProperty
        /// </summary>
        public Propertys.WidthProperty WidthProperty { get { return _WidthProperty; } }

        #region internal

        //internal
        internal int Id { get; set; }
        internal int ParentId { get; set; }
        internal bool PointerIsEntered { get; set; }

        #endregion

        #region Width Height

        internal Propertys.WidthProperty _WidthProperty;
        internal Propertys.HeightProperty _HeightProperty;

        /// <summary>
        /// Width
        /// </summary>
        public double Width
        {
            get { return (double)_WidthProperty.GetValue(); }
            set { _WidthProperty.SetValue(value); SizeChangedTrigger(); }
        }

        /// <summary>
        /// Height
        /// </summary>
        public double Height
        {
            get { return (double)_HeightProperty.GetValue(); }
            set { _HeightProperty.SetValue(value); SizeChangedTrigger(); }
        }

        #endregion
        
        #region Children

        /// <summary>
        /// Get or set child elements to parent
        /// </summary>
        public Collections.ChildrenCollection Children { get; set; }

        #endregion

        #region Events

        #region PointerMoved

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler PointerMoved;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnPointerMoved(PointerRoutedEventArgs e)
        {
            PointerMoved?.Invoke(this, e);
        }
        internal void PointerMovedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerMoved(e);
        }

        #endregion

        #region Entered

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler PointerEntered;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnPointerEntered(PointerRoutedEventArgs e)
        {
            PointerEntered?.Invoke(this, e);
        }
        internal void PointerEnteredTrigger(PointerRoutedEventArgs e)
        {
            OnPointerEntered(e);
        }

        #endregion

        #region Exited

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler PointerExited;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnPointerExited(PointerRoutedEventArgs e)
        {
            PointerExited?.Invoke(this, e);
        }
        internal void PointerExitedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerExited(e);
        }

        #endregion

        #region Pressed

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler PointerPressed;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnPointerPressed(PointerRoutedEventArgs e)
        {
            PointerPressed?.Invoke(this, e);
        }
        internal void PointerPressedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerPressed(e);
        }

        #endregion

        #region PointerReleased

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler PointerReleased;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnPointerReleased(PointerRoutedEventArgs e)
        {
            PointerReleased?.Invoke(this, e);
        }
        internal void PointerReleasedTrigger(PointerRoutedEventArgs e)
        {
            OnPointerReleased(e);
        }

        #endregion

        #region Tapped

        /// <summary>
        /// Tapped/Click. Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler Tapped;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnTapped(PointerRoutedEventArgs e)
        {
            Tapped?.Invoke(this, e);
        }
        internal void TappedTrigger(PointerRoutedEventArgs e)
        {
            OnTapped(e);
        }

        #endregion

        #region RightTapped

        /// <summary>
        /// Tapped/Click. Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler RightTapped;

        /// <summary>
        /// Tapped/Click. Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnRightTapped(PointerRoutedEventArgs e)
        {
            RightTapped?.Invoke(this, e);
        }
        internal void RightTappedTrigger(PointerRoutedEventArgs e)
        {
            OnRightTapped(e);
        }

        #endregion

        #region SwipeLeft

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler SwipeLeft;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnSwipeLeft(PointerRoutedEventArgs e)
        {
            SwipeLeft?.Invoke(this, e);
        }
        internal void SwipeLeftTrigger(PointerRoutedEventArgs e)
        {
            OnSwipeLeft(e);
        }

        #endregion

        #region SwipeRight

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler SwipeRight;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnSwipeRight(PointerRoutedEventArgs e)
        {
            SwipeRight?.Invoke(this, e);
        }
        internal void SwipeRightTrigger(PointerRoutedEventArgs e)
        {
            OnSwipeRight(e);
        }

        #endregion

        #region SwipeTop

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler SwipeTop;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnSwipeTop(PointerRoutedEventArgs e)
        {
            SwipeTop?.Invoke(this, e);
        }
        internal void SwipeTopTrigger(PointerRoutedEventArgs e)
        {
            OnSwipeTop(e);
        }

        #endregion

        #region SwipeBottom

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        public event PointerRoutedEventHandler SwipeBottom;

        /// <summary>
        /// Routed event for mouse, touch, pen
        /// </summary>
        protected virtual void OnSwipeBottom(PointerRoutedEventArgs e)
        {
            SwipeBottom?.Invoke(this, e);
        }
        internal void SwipeBottomTrigger(PointerRoutedEventArgs e)
        {
            OnSwipeBottom(e);
        }

        #endregion

        #region SizeChanged

        /// <summary>
        /// Occurs when either the ActualHeight or the ActualWidth property changes 
        /// </summary>
        public event SizeChangedEventHandler SizeChanged;

        /// <summary>
        /// Occurs when either the ActualHeight or the ActualWidth property changes 
        /// </summary>
        protected virtual void OnSizeChanged(SizeChangedEventArgs e)
        {
            SizeChanged?.Invoke(this, e);
        }
        internal void SizeChangedTrigger()
        {
            SizeChangedEventArgs e = new SizeChangedEventArgs()
            {
                NewHeight = Height,
                NewWidth = Width
            };
            OnSizeChanged(e);
        }

        #endregion

        #endregion
    }
}
