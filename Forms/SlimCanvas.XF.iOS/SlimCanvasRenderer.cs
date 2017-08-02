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
    /// <summary>
    /// Create new Canvas in Xamarin Forms
    /// </summary>
    public class SlimCanvasXFiOS : ViewRenderer<SlimCanvasXF, SlimCanvasIOS>
    {
        /// <summary>
        /// Call befor Forms.Init
        /// </summary>
        public static void Initialize()
        {

        }

        SlimCanvasIOS slim;


        /// <summary>
        /// to be added
        /// </summary>
        /// <param name="e"></param>
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
