using SlimCanvas;
using SlimCanvas.iOS;
using SlimCanvas.XF;
using SlimCanvas.XF.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SlimCanvasXF), typeof(SlimCanvasXFiOS))]
namespace SlimCanvas.XF.iOS
{
    public class SlimCanvasXFiOS : ViewRenderer<SlimCanvasXF, SlimCanvasIOS>
    {
        public static void Initialize()
        {

        }

        SlimCanvasIOS slim;

        protected override void OnElementChanged(ElementChangedEventArgs<SlimCanvasXF> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                slim = new SlimCanvasIOS();
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
