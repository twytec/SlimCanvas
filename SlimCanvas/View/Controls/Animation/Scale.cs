using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.View.Controls.Animation
{
    /// <summary>
    /// Scale animation
    /// </summary>
    public class Scale : AnimateBase
    {
        double startX;
        double startY;
        double endX;
        double endY;
        Vector2 to;

        /// <summary>
        /// Create scale animation
        /// </summary>
        /// <param name="element"></param>
        /// <param name="to"></param>
        /// <param name="duration"></param>
        /// <param name="met"></param>
        public Scale(UIElement element, Vector2 to, double duration, AnimateMethode met)
        {
            startX = element.Scale.X;
            startY = element.Scale.Y;
            endX = to.X - startX;
            endY = to.Y - startY;
            this.to = to;
            this.basicElement = element;
            this.duration = duration;
            this.met = met;
        }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="e"></param>
        protected internal override void Update(RenderingEventArgs e)
        {
            base.Update(e);

            if (durationSteps <= duration)
            {
                var x = AnimateInternal.GetValue(durationSteps, startX, endX, duration, met);
                var y = AnimateInternal.GetValue(durationSteps, startY, endY, duration, met);

                basicElement.Scale = new Vector2(x, y); 
                durationSteps += e.ElapseTime.TotalMilliseconds;
            }
            else
            {
                basicElement.Scale = new Vector2(to.X, to.Y);
                OnAnimationFinish();
            }
        }

        /// <summary>
        /// Reset to start values
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            basicElement.Scale = new Vector2(startX, startY);
        }
    }
}
