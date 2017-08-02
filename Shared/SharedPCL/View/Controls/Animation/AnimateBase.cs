using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Animation
{
    /// <summary>
    /// AnimateBase
    /// </summary>
    public abstract class AnimateBase
    {
        internal double duration;
        internal double durationSteps = 0;
        internal AnimateMethode met;
        internal bool endlessLoop;
        internal UIElement basicElement;

        System.Threading.EventWaitHandle waitHandle;

        /// <summary>
        /// Event to antmation finish
        /// </summary>
        public event EventHandler AnimationFinish;

        /// <summary>
        /// Animation finish trigger
        /// </summary>
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

        /// <summary>
        /// Start animation
        /// </summary>
        public void Start()
        {
            Canvas.MyCanvas.Rendering += MyCanvas_Rendering;
        }

        /// <summary>
        /// Start animation an await for finish
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            Canvas.MyCanvas.Rendering += MyCanvas_Rendering;
            
            await Task.Run(() =>
            {
                waitHandle = new System.Threading.AutoResetEvent(false);
                waitHandle.WaitOne();
            });

        }

        /// <summary>
        /// Stop animation
        /// </summary>
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

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="e"></param>
        internal protected virtual void Update(EventTypes.RenderingEventArgs e)
        {
            
        }

        /// <summary>
        /// Reset to start value
        /// </summary>
        public virtual void Reset()
        {
            durationSteps = 0;
        }
    }
}
