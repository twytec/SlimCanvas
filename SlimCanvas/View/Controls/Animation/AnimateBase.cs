using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Animation
{
    public abstract class AnimateBase
    {
        internal double duration;
        internal double durationSteps = 0;
        internal AnimateMethode met;
        internal bool endlessLoop;
        internal UIElement basicElement;

        System.Threading.EventWaitHandle waitHandle;

        public event EventHandler AnimationFinish;
        protected virtual void OnAnimationFinish()
        {
            if (endlessLoop)
            {
                Reset();
            }
            else
            {
                Stop();
                AnimationFinish?.Invoke(new object(), new EventArgs());
            }
        }

        public void Start()
        {
            Canvas.MyCanvas.Rendering += MyCanvas_Rendering;
        }

        public async Task StartAsync()
        {
            Canvas.MyCanvas.Rendering += MyCanvas_Rendering;
            
            await Task.Run(() =>
            {
                waitHandle = new System.Threading.AutoResetEvent(false);
                waitHandle.WaitOne();
            });

        }

        public void Stop()
        {
            Canvas.MyCanvas.Rendering -= MyCanvas_Rendering;
            if (waitHandle != null)
                waitHandle.Set();
        }

        void MyCanvas_Rendering(object sender, EventTypes.RenderingEventArgs e)
        {
            Update(e);
        }

        internal protected virtual void Update(EventTypes.RenderingEventArgs e)
        {
            
        }

        public virtual void Reset()
        {
            durationSteps = 0;
        }
    }
}
