using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.View.Controls.Animation
{
    /// <summary>
    /// Rotate animation
    /// </summary>
    public class Rotate : AnimateBase
    {
        double startValue;
        double endValue;
        double degress;
        double dps;
        bool rotatePerSecond = false;
        bool backward = false;
        
        /// <summary>
        /// Create new rotate animation
        /// </summary>
        /// <param name="element"></param>
        /// <param name="degress"></param>
        /// <param name="duration"></param>
        /// <param name="met"></param>
        /// <param name="endlessLoop"></param>
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
        /// <param name="element"></param>
        /// <param name="dps">degress to second</param>
        /// <param name="backward">backward clockwise</param>
        public Rotate(UIElement element, double dps, bool backward = false)
        {
            this.basicElement = element;
            this.dps = dps / 1000;
            rotatePerSecond = true;
            this.backward = backward;
        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// Reset to start values
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            basicElement.Rotation = startValue;
        }
    }
}
