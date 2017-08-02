using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SlimCanvas.XF
{
    /// <summary>
    /// SlimCanvas Xamarin Forms
    /// </summary>
    public class SlimCanvasXF : Xamarin.Forms.View
    {
        /// <summary>
        /// Handler for CanvasLoaded event
        /// </summary>
        /// <param name="slimCanvasPCL"></param>
        public delegate void CanvasLoadedEventHandler(Canvas slimCanvasPCL);

        /// <summary>
        /// Occurs if Canvas is complet loaded
        /// </summary>
        public event CanvasLoadedEventHandler CanvasLoaded;

        /// <summary>
        /// Occurs if Canvas is complet loaded
        /// </summary>
        protected virtual void OnCanvasLoaded(Canvas slimCanvasPCL)
        {
            CanvasLoaded?.Invoke(slimCanvasPCL);
        }

        /// <summary>
        /// Trigger
        /// </summary>
        public void CanvasLoadedTrigger(Canvas slimCanvasPCL)
        {
            OnCanvasLoaded(slimCanvasPCL);
        }
    }
}
