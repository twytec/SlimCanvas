using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    public class Canvas : View.Controls.BasicElement
    {
        internal static Canvas MyCanvas;
        internal static View.Draw ViewDraw;
        internal static View.Controls.Collections.GlobalChildrenList GlobalChildrenList;

        
        public Plattform Plattform { get; set; }
        public View.Camera Camera { get; private set; }
        public Abstractions.IAssets Assets { get; private set; }
        public Abstractions.IGraphics Graphics { get; private set; }

        public View.Brush Background { get { return _backgroundBrush; } set { _backgroundBrush = value; BackroundRect.FillBrush = value; } }
        View.Brush _backgroundBrush = View.SolidColorBrush.White;
        internal View.Controls.Primitive.Rectangle BackroundRect;

        /// <summary>
        /// Set automatically the canvas size. Default is true
        /// </summary>
        public bool AutoResize { get; set; } = true;

        public event View.Controls.EventTypes.RenderingEventHandler Rendering;
        protected virtual void OnRendering(View.Controls.EventTypes.RenderingEventArgs e)
        {
            Rendering?.Invoke(this, e);
        }
        internal void RenderingTriger(TimeSpan totalTime, TimeSpan elapseTime)
        {
            View.Controls.EventTypes.RenderingEventArgs e = new View.Controls.EventTypes.RenderingEventArgs()
            {
                ElapseTime = elapseTime,
                TotalElapesTime = totalTime
            };

            OnRendering(e);
        }
        
        public Canvas(Abstractions.ISlimDraw iDraw, Abstractions.IUserInput iInput, Abstractions.IAssets assets, Abstractions.IGraphics graphics)
        {
            MyCanvas = this;
            Assets = assets;
            Graphics = graphics;
            
            Camera = new View.Camera();
            
            ViewDraw = new View.Draw(iDraw);
            ViewDraw.UpdateView();
            iDraw.ViewSizeChanged += IDraw_SizeChanged;

            Controller.UserInput userInput = new Controller.UserInput(iInput);

            BackroundRect = new View.Controls.Primitive.Rectangle()
            {
                Thickness = 0,
                FillBrush = _backgroundBrush
            };

            WidthProperty.PropertyChanged += WidthProperty_PropertyChanged;
            HeightProperty.PropertyChanged += HeightProperty_PropertyChanged;
        }

        private void HeightProperty_PropertyChanged(object sender, EventArgs e)
        {
            BackroundRect.Height = Height;
        }

        private void WidthProperty_PropertyChanged(object sender, EventArgs e)
        {
            BackroundRect.Width = Width;
        }

        private void IDraw_SizeChanged(object sender, View.Controls.EventTypes.SizeChangedEventArgs e)
        {
            ViewDraw.UpdateView();
            
            if (AutoResize)
            {
                this.Width = e.NewWidth;
                this.Height = e.NewHeight;
                BackroundRect.Width = e.NewWidth;
                BackroundRect.Height = e.NewHeight;
                BackroundRect.ActualWidth = e.NewWidth;
                BackroundRect.ActualHeight = e.NewHeight;
            }
            
            Camera.Width = e.NewWidth;
            Camera.Height = e.NewHeight;

            OnSizeChanged(e);
        }

        public void Clear()
        {
            
            Children.Clear();
            Background = View.SolidColorBrush.White;
        }
    }
}
