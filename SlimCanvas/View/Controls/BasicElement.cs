using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls
{
    public class BasicElement
    {
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

        public Propertys.HeightProperty HeightProperty { get { return _HeightProperty; } }
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

        public double Width
        {
            get { return (double)_WidthProperty.GetValue(); }
            set { _WidthProperty.SetValue(value); SizeChangedTrigger(); }
        }
        public double Height
        {
            get { return (double)_HeightProperty.GetValue(); }
            set { _HeightProperty.SetValue(value); SizeChangedTrigger(); }
        }

        #endregion
        
        #region Children

        public Collections.ChildrenCollection Children { get; set; }

        #endregion

        #region Events

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

        #region Entered

        public event PointerRoutedEventHandler PointerEntered;
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

        public event PointerRoutedEventHandler PointerExited;
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

        #region Tapped

        public event PointerRoutedEventHandler Tapped;
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

        public event PointerRoutedEventHandler RightTapped;
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

        public event PointerRoutedEventHandler SwipeLeft;
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

        public event PointerRoutedEventHandler SwipeRight;
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

        public event PointerRoutedEventHandler SwipeTop;
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

        public event PointerRoutedEventHandler SwipeBottom;
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

        public event SizeChangedEventHandler SizeChanged;
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
