using SlimCanvas;
using SlimCanvas.Droid;
using SlimCanvas.XF;
using SlimCanvas.XF.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SlimCanvasXF), typeof(SlimCanvasXFDroid))]
namespace SlimCanvas.XF.Droid
{
    public class SlimCanvasXFDroid : ViewRenderer<SlimCanvasXF, SlimCanvasDroid>
    {
        public static void Initialize()
        {

        }

        SlimCanvasDroid slim;

        protected override void OnElementChanged(ElementChangedEventArgs<SlimCanvasXF> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                slim = new SlimCanvasDroid(this.Context);
                SetNativeControl(slim);
            }

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                e.NewElement.CanvasLoadedTrigger(slim.SlimCanvasPCL);
            }
        }
    }
}
