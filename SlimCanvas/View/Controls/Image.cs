using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls
{
    /// <summary>
    /// to be added
    /// </summary>
    public class Image : UIElement
    {
        /// <summary>
        /// to be added
        /// </summary>
        public Image()
        {
            _ClipProperty = new Propertys.ClipProperty(this);
        }

        /// <summary>
        /// to be added
        /// </summary>
        public Propertys.ClipProperty ClipProperty { get { return _ClipProperty; } }

        internal Propertys.ClipProperty _ClipProperty;

        /// <summary>
        /// to be added
        /// </summary>
        public Rect Clip
        {
            get { return (Rect)_ClipProperty.GetValue(); }
            set { _ClipProperty.SetValue(value); }
        }

        Abstractions.IBitmap _iBitmap;

        /// <summary>
        /// to be added
        /// </summary>
        public Abstractions.IBitmap Source
        {
            get
            {
                return _iBitmap;
            }
            set
            {
                _iBitmap = value;
                if (Width == 0 || Height == 0)
                {
                    Width = _iBitmap.Width;
                    Height = _iBitmap.Height;
                    Clip = new Rect(0, 0, Width, Height);
                }
            }
        }
    }
}
