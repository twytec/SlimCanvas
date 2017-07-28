using SlimCanvas.View.Controls.EventTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls
{
    public class UIElement : BasicElement, IDisposable
    {
        public UIElement()
        {
            _XProperty = new Propertys.XProperty(this);
            _YProperty = new Propertys.YProperty(this);
            _RotationProperty = new Propertys.RotationProperty(this);
            _OpacityProperty = new Propertys.OpacityProperty(this);
            _OrginProperty = new Propertys.OriginProperty(this);
            _ScaleProperty = new Propertys.ScaleProperty(this);
            _ZProperty = new Propertys.ZProperty(this);
            _ParentProperty = new Propertys.ParentProperty(this);
            
            SizeChanged += BasicElement_SizeChanged;
            _ParentProperty.PropertyChanged += _ParentProperty_PropertyChanged;
        }

        private void _ParentProperty_PropertyChanged(object sender, EventArgs e)
        {
            InitActualSize();
        }

        private void BasicElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetActualSize();
        }

        public object Tag { get; set; }

        #region Propertys

        public Propertys.XProperty XProperty { get { return _XProperty; } }
        public Propertys.YProperty YProperty { get { return _YProperty; } }
        public Propertys.RotationProperty RotationProperty { get { return _RotationProperty; } }
        public Propertys.OpacityProperty OpacityProperty { get { return _OpacityProperty; } }
        public Propertys.OriginProperty OriginProperty { get { return _OrginProperty; } }
        public Propertys.ScaleProperty ScaleProperty { get { return _ScaleProperty; } }
        public Propertys.ZProperty ZIndexProperty { get { return _ZProperty; } }
        public Propertys.ParentProperty ParentProperty { get { return _ParentProperty; } }

        #endregion

        #region Internal

        internal Propertys.XProperty _XProperty;
        internal Propertys.YProperty _YProperty;
        internal Propertys.RotationProperty _RotationProperty;
        internal Propertys.OpacityProperty _OpacityProperty;
        internal Propertys.OriginProperty _OrginProperty;
        internal Propertys.ScaleProperty _ScaleProperty;
        internal Propertys.ZProperty _ZProperty;


        #endregion

        #region Parent
        
        internal Propertys.ParentProperty _ParentProperty;

        public BasicElement Parent
        {
            get { return _ParentProperty.GetValue() as BasicElement; }
            set { _ParentProperty.SetValue(value); }
        }

        #endregion

        #region Horizontal Vertical Alignment Margin

        public EnumTypes.HorizontalAlignment HorizontalAlignment { get { return _horizontalAlignment; } set { _horizontalAlignment = value; SetActualSize(); } }
        EnumTypes.HorizontalAlignment _horizontalAlignment;

        public EnumTypes.VerticalAlignment VerticalAlignment { get { return _verticalAlignment; } set { _verticalAlignment = value; SetActualSize(); } }
        EnumTypes.VerticalAlignment _verticalAlignment;

        /// <summary>
        /// This is any work with Horizontal- and VerticalAlignment
        /// </summary>
        public Margin Margin { get; set; }

        #endregion

        #region Actual width, height, x and y

        public double ActualWidth { get; set; }
        public double ActualHeight { get; set; }
        public double ActualX { get; set; }
        public double ActualY { get; set; }
        
        void InitActualSize()
        {
            Parent.SizeChanged += Parent_SizeChanged;
            SetActualSize();
        }
        
        internal void SetActualSize()
        {
            if (Parent == null)
                return;

            ActualWidth = Width * Scale.X;
            ActualHeight = Height * Scale.Y;

            double parentAW = 0;
            double parentAH = 0;

            if (Parent is UIElement p)
            {
                parentAW = p.ActualWidth;
                parentAH = p.ActualHeight;
            }
            else
            {
                parentAH = Parent.Height;
                parentAW = Parent.Width;
            }

            switch (HorizontalAlignment)
            {
                case EnumTypes.HorizontalAlignment.None:
                    ActualX = X * Scale.X;
                    break;
                case EnumTypes.HorizontalAlignment.Left:
                    ActualX = 0 + Margin.Left;
                    break;
                case EnumTypes.HorizontalAlignment.Center:
                    ActualX = (parentAW - ActualWidth) / 2;
                    break;
                case EnumTypes.HorizontalAlignment.Right:
                    ActualX = parentAW - ActualWidth - Margin.Right;
                    break;
            }

            switch (VerticalAlignment)
            {
                case EnumTypes.VerticalAlignment.None:
                    ActualY = Y * Scale.Y;
                    break;
                case EnumTypes.VerticalAlignment.Top:
                    ActualY = 0 + Margin.Top;
                    break;
                case EnumTypes.VerticalAlignment.Center:
                    ActualY = (parentAH - ActualHeight) / 2;
                    break;
                case EnumTypes.VerticalAlignment.Bottom:
                    ActualY = parentAH - ActualHeight - Margin.Bottom;
                    break;
            }

            if (Children.Count() > 0)
            {
                foreach (var item in Children)
                {
                    item.SetActualSize();
                }
            }
        }

        private void Parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetActualSize();
        }

        #endregion

        #region X, Y

        public double X
        {
            get { return (double)_XProperty.GetValue(); }
            set { _XProperty.SetValue(value); SetActualSize(); }
        }
        public double Y
        {
            get { return (double)_YProperty.GetValue(); }
            set { _YProperty.SetValue(value); SetActualSize(); }
        }

        #endregion
        
        #region Transform

        /// <summary>
        /// Value in degrees 0-360°
        /// </summary>
        public double Rotation
        {
            get { return (double)_RotationProperty.GetValue(); }
            set { _RotationProperty.SetValue(value); }
        }

        /// <summary>
        /// Value 0.0-1.0
        /// </summary>
        public double Opacity
        {
            get { return (double)_OpacityProperty.GetValue(); }
            set { _OpacityProperty.SetValue(value); }
        }

        /// <summary>
        /// Value 0-1. 0,0 is upper-left, 0.5,0.5 is center
        /// </summary>
        public Vector2 Origin
        {
            get { return (Vector2)_OrginProperty.GetValue(); }
            set { _OrginProperty.SetValue(value); }
        }

        /// <summary>
        /// Value 0.0-X.X
        /// </summary>
        public Vector2 Scale
        {
            get { return (Vector2)_ScaleProperty.GetValue(); }
            set { _ScaleProperty.SetValue(value); SetActualSize(); }
        }

        #endregion
        
        public int ZIndex
        {
            get { return (int)_ZProperty.GetValue(); }
            set { _ZProperty.SetValue(value); }
        }
        
        public void Dispose()
        {
            SizeChanged -= BasicElement_SizeChanged;
            _ParentProperty.PropertyChanged -= _ParentProperty_PropertyChanged;

            if (Parent != null)
                Parent.SizeChanged -= Parent_SizeChanged;

            if (this is Image img)
            {
                img.Source.Dispose();
            }
        }
    }
}
