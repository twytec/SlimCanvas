using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using SlimCanvas.View.Controls.EventTypes;
using SlimCanvas.View.Controls.Primitive;
using SlimCanvas.View.Controls;
using CoreText;

namespace SlimCanvas.iOS
{
    /// <summary>
    /// Create new Canvas on UWP
    /// </summary>
    public class SlimCanvasIOS : UIView
    {
        /// <summary>
        /// This is the portable Canvas
        /// </summary>
        public Canvas SlimCanvasPCL;
        

        DrawInCanvas drawInCanvas;
        IUserInput uInput;

        /// <summary>
        /// Create new Canvas on UWP
        /// </summary>
        public SlimCanvasIOS()
        {
            UserInteractionEnabled = true;
            MultipleTouchEnabled = true;

            drawInCanvas = new DrawInCanvas();
            uInput = new IUserInput(this);

            SlimCanvasPCL = new Canvas(drawInCanvas, uInput, new IAssetsiOS(), new IGraphicsiOS())
            {
                Platform = Plattform.IOS
            };
        }

        /// <summary>
        /// to be added
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            
            drawInCanvas.imageView = new UIImageView(new CGRect(0,0, Frame.Width, Frame.Height));
            this.AddSubview(drawInCanvas.imageView);
        }

        #region UserInput

        /// <summary>
        /// to be added
        /// </summary>
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            uInput.TouchesBegan(touches, evt);
        }

        /// <summary>
        /// to be added
        /// </summary>
        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            uInput.TouchesBegan(touches, evt);
        }

        /// <summary>
        /// to be added
        /// </summary>
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            uInput.TouchesBegan(touches, evt);
        }

        #endregion

        #region Size changed

        /// <summary>
        /// to be added
        /// </summary>
        public override CGRect Frame
        {
            get => base.Frame;
            set
            {
                base.Frame = value;
                drawInCanvas?.ViewSizeChangedTrigger((int)value.Width, (int)value.Height);
            }
        }

        /// <summary>
        /// to be added
        /// </summary>
        public override CGRect Bounds
        {
            get => base.Bounds;
            set
            {
                base.Bounds = value;
                drawInCanvas?.ViewSizeChangedTrigger((int)value.Width, (int)value.Height);
            }
        }

        #endregion
    }
}