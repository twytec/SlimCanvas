using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.View.Controls.Animation
{
    public class Rotate : AnimateBase
    {
        double startValue;
        double endValue;
        double degress;
        double dps;
        bool rotatePerSecond = false;
        bool backward = false;
        
        public Rotate(UIElement element, double degress, double duration, AnimateMethode met, bool endlessLoop = false)
        {
            startValue = element.Rotation;
            endValue = degress - startValue;
            this.degress = degress;
            this.basicElement = element;
            this.duration = duration;
            this.met = met;
            this.endlessLoop = endlessLoop;
        }

        /// <summary>
        /// Rotation animation loop
        /// </summary>
        /// <param name="element">BasicElement</param>
        /// <param name="dps">Degress per second</param>
        public Rotate(UIElement element, double dps, bool backward = false)
        {
            this.basicElement = element;
            this.dps = dps / 1000;
            rotatePerSecond = true;
            this.backward = backward;
        }

        protected internal override void Update(RenderingEventArgs e)
        {
            base.Update(e);

            if (rotatePerSecond)
            {
                if (backward)
                    basicElement.Rotation -= dps * e.ElapseTime.TotalMilliseconds;
                else
                    basicElement.Rotation += dps * e.ElapseTime.TotalMilliseconds;
            }
            else
            {
                if (durationSteps <= duration)
                {
                    basicElement.Rotation = AnimateInternal.GetValue(durationSteps, startValue, endValue, duration, met);
                    durationSteps += e.ElapseTime.TotalMilliseconds;
                }
                else
                {
                    basicElement.Rotation = degress;
                    OnAnimationFinish();
                }
            }
            
        }

        public override void Reset()
        {
            base.Reset();
            basicElement.Rotation = startValue;
        }
    }
}
