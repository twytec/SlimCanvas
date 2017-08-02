using SlimCanvas;
using SlimCanvas.UWP;
using SlimCanvas.XF;
using SlimCanvas.XF.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(SlimCanvasXF), typeof(SlimCanvasXFUWP))]
namespace SlimCanvas.XF.UWP
{
    public class SlimCanvasXFUWP : ViewRenderer<SlimCanvasXF, SlimCanvasUWP>
    {
        public static void Initialize()
        {

        }

        SlimCanvasUWP slim;

        protected override void OnElementChanged(ElementChangedEventArgs<SlimCanvasXF> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                slim = new SlimCanvasUWP();
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
