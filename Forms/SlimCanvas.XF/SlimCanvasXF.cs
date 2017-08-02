using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SlimCanvas.XF
{
    public class SlimCanvasXF : Xamarin.Forms.View
    {
        public delegate void CanvasLoadedEventHandler(Canvas slimCanvasPCL);

        public event CanvasLoadedEventHandler CanvasLoaded;
        protected virtual void OnCanvasLoaded(Canvas slimCanvasPCL)
        {
            CanvasLoaded?.Invoke(slimCanvasPCL);
        }
        public void CanvasLoadedTrigger(Canvas slimCanvasPCL)
        {
            OnCanvasLoaded(slimCanvasPCL);
        }
    }
}
