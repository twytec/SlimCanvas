using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View
{
    /// <summary>
    /// View camera
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Width
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// X can not less than 0 and greater than Canvas.Width - Camera.Width
        /// </summary>
        public double X
        {
            get { return _x; }
            set
            {
                var maxRight = Canvas.MyCanvas.Width - Width;
                if (value < 0)
                    _x = 0;
                else if (value > maxRight)
                    _x = maxRight;
                else
                    _x = value;
            }
        }

        /// <summary>
        /// Y can not less than 0 and greater than Canvas.Height - Camera.Height
        /// </summary>
        public double Y
        {
            get { return _y; }
            set
            {
                var maxBottom = Canvas.MyCanvas.Height - Height;
                if (value < 0)
                    _y = 0;
                else if (value > maxBottom)
                    _y = maxBottom;
                else
                    _y = value;
            }
        }

        double _x = 0;
        double _y = 0;
    }
}
