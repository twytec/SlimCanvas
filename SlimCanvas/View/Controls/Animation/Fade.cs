using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.View.Controls.Animation
{
    public class Fade : AnimateBase
    {
        double startValue;
        double endValue;
        double endOpacity;

        public Fade(UIElement element, double endOpacity, double duration, AnimateMethode met, bool endlessLoop = false)
        {
            startValue = element.Opacity;
            endValue = endOpacity - startValue;
            this.endOpacity = endOpacity;
            this.basicElement = element;
            this.duration = duration;
            this.met = met;
            this.endlessLoop = endlessLoop;
        }

        protected internal override void Update(RenderingEventArgs e)
        {
            base.Update(e);

            if (durationSteps <= duration)
            {
                basicElement.Opacity = AnimateInternal.GetValue(durationSteps, startValue, endValue, duration, met);
                durationSteps += e.ElapseTime.TotalMilliseconds;
            }
            else
            {
                basicElement.Opacity = endOpacity;
                OnAnimationFinish();
            }
        }

        public override void Reset()
        {
            base.Reset();
            basicElement.Opacity = startValue;
        }
    }
}
