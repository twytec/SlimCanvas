using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.EventTypes;

namespace SlimCanvas.View.Controls.Animation
{
    public class Animate : AnimateBase
    {
        private class AniModel
        {
            public Propertys.BasicProperty Property { get; set; }
            public double EndValue { get; set; }
            public double StartValue { get; set; }
            public double StepValue { get; set; }
        }

        List<AniModel> myList;

        public Animate(Propertys.BasicProperty property, double endValue, double duration, AnimateMethode met, bool endlessLoop)
            : this (new Dictionary<Propertys.BasicProperty, double>() { { property, endValue} }, duration, met, endlessLoop)
        {
        }

        public Animate(Dictionary<Propertys.BasicProperty, double> list, double duration, AnimateMethode met, bool endlessLoop)
        {
            myList = new List<AniModel>();

            foreach (var item in list)
            {
                myList.Add(new AniModel() {
                    EndValue = item.Value,
                    Property = item.Key,
                    StartValue = (double)item.Key.GetValue(),
                    StepValue = item.Value - (double)item.Key.GetValue()
                });
            }
            
            this.duration = duration;
            this.met = met;
            this.endlessLoop = endlessLoop;
        }

        protected internal override void Update(RenderingEventArgs e)
        {
            base.Update(e);

            if (durationSteps <= duration)
            {
                foreach (var item in myList)
                {
                    item.Property.SetValue(AnimateInternal.GetValue(durationSteps, item.StartValue, item.StepValue, duration, met));
                }
                durationSteps += e.ElapseTime.TotalMilliseconds;
            }
            else
            {
                foreach (var item in myList)
                {
                    item.Property.SetValue(item.EndValue);
                }
                OnAnimationFinish();
            }
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var item in myList)
            {
                item.Property.SetValue(item.StartValue);
            }
        }
    }
}
