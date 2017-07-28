using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.View.Controls.Animation
{
    public class MoveTo : AnimateBase
    {
        double startX;
        double startY;
        double endX;
        double endY;
        Vector2 to;

        public MoveTo(UIElement element, Vector2 to, double duration, AnimateMethode met)
        {
            startX = element.X;
            startY = element.Y;
            endX = to.X - startX;
            endY = to.Y - startY;
            this.to = to;
            this.basicElement = element;
            this.duration = duration;
            this.met = met;
        }

        protected internal override void Update(RenderingEventArgs e)
        {
            base.Update(e);
            if (durationSteps <= duration)
            {
                basicElement.X = AnimateInternal.GetValue(durationSteps, startX, endX, duration, met);
                basicElement.Y = AnimateInternal.GetValue(durationSteps, startY, endY, duration, met);
                durationSteps += e.ElapseTime.TotalMilliseconds;
            }
            else
            {
                basicElement.X = to.X;
                basicElement.Y = to.Y;
                OnAnimationFinish();
            }
        }

        public override void Reset()
        {
            base.Reset();
            basicElement.X = startX;
            basicElement.Y = startY;
        }
    }
}
