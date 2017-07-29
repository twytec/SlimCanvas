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
using Android.Util;
using Android.Graphics;
using SlimCanvas.View.Controls;
using SlimCanvas.View.Controls.EventTypes;
using SlimCanvas.View.Controls.Primitive;
using System.Threading.Tasks;

namespace SlimCanvas.Droid
{
    /// <summary>
    /// Create new Canvas on UWP
    /// </summary>
    public class SlimCanvasDroid : SurfaceView, ISurfaceHolderCallback
    {
        /// <summary>
        /// This is the portable Canvas
        /// </summary>
        public Canvas SlimCanvasPCL;

        Context context;
        DrawInCanvas drawInCanvas;

        /// <summary>
        /// Create new Canvas on UWP
        /// </summary>
        public SlimCanvasDroid(Context context) : base(context)
        {
            this.context = context;
            Holder.AddCallback(this);

            drawInCanvas = new DrawInCanvas(this, context);

            SlimCanvasPCL = new Canvas(drawInCanvas, new IUserInputDroid(this), new IAssetsDroid(context), new IGraphicsDroid())
            {
                Platform = Plattform.Android
            };
            //this.Focusable = true;
            //FocusableInTouchMode = true;
        }

        #region Size changed

        /// <summary>
        /// to be added
        /// </summary>
        public void SurfaceChanged(ISurfaceHolder holder, Android.Graphics.Format format, int width, int height)
        {
            drawInCanvas.ViewSizeChangedTrigger(width, height);
        }

        #endregion

        #region SurfaceCreated SurfaceDestroyed

        /// <summary>
        /// to be added
        /// </summary>
        public void SurfaceCreated(ISurfaceHolder holder)
        {
            drawInCanvas.holder = holder;
            Task.Run(() => drawInCanvas.Run());
        }

        /// <summary>
        /// to be added
        /// </summary>
        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            drawInCanvas.runThread = false;
        }

        #endregion
    }
}