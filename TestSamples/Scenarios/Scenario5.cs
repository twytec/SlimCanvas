using System;
using System.Collections.Generic;
using System.Text;
using SlimCanvas;
using System.Threading.Tasks;

namespace TestSamples.Scenarios
{
    public class Scenario5 : ISamples
    {
        Canvas canvas;
        double width = 1000;

        SlimCanvas.View.Controls.Primitive.Ellipse ellipse1;
        SlimCanvas.View.Controls.Primitive.Ellipse ellipse2;
        SlimCanvas.View.Controls.Primitive.Rectangle rect;
        SlimCanvas.View.Controls.Primitive.Path path;

        public void LoadScenario(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.Clear();
            canvas.Background = new SlimCanvas.View.SolidColorBrush(new SlimCanvas.Color(47, 47, 47, 255));
            canvas.AutoResize = true;

            #region Ellipse AnimateEllpise

            ellipse1 = new SlimCanvas.View.Controls.Primitive.Ellipse()
            {
                Width = 400,
                Height = 400,
                Thickness = 5,
                StrokeColor = SlimCanvas.Color.White,
                StrokeStyle = SlimCanvas.View.Controls.EnumTypes.DashStyle.Dash,
                FillBrush = new SlimCanvas.View.SolidColorBrush(Color.Transparent),
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                Origin = new Vector2(0.5d, 0.5d)
            };

            ellipse2 = new SlimCanvas.View.Controls.Primitive.Ellipse()
            {
                Width = 370,
                Height = 370,
                Thickness = 5,
                StrokeColor = SlimCanvas.Color.White,
                StrokeStyle = SlimCanvas.View.Controls.EnumTypes.DashStyle.Dash,
                FillBrush = new SlimCanvas.View.SolidColorBrush(Color.Transparent),
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                Origin = new Vector2(0.5d, 0.5d)
            };

            ellipse1.Children.Add(ellipse2);
            canvas.Children.Add(ellipse1);

            #endregion

            #region Rectangle AnimateRect

            rect = new SlimCanvas.View.Controls.Primitive.Rectangle()
            {
                Width = 50,
                Height = 50,
                FillBrush = new SlimCanvas.View.SolidColorBrush(Color.BlueViolet)
            };
            canvas.Children.Add(rect);

            #endregion

            #region Arrow PrpoertyAnimation

            path = new SlimCanvas.View.Controls.Primitive.Path()
            {
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                Origin = new Vector2(0.5d, 0.5d)
            };
            path.AddPoint(new SlimCanvas.Vector2(23, 0));
            path.AddPoint(new SlimCanvas.Vector2(46, 58));
            path.AddPoint(new SlimCanvas.Vector2(35, 60));
            path.AddPoint(new SlimCanvas.Vector2(35, 170));
            path.AddPoint(new SlimCanvas.Vector2(11, 170));
            path.AddPoint(new SlimCanvas.Vector2(11, 60));
            path.AddPoint(new SlimCanvas.Vector2(0, 58));
            path.AddPoint(new SlimCanvas.Vector2(23, 0));
            canvas.Children.Add(path);

            #endregion
            
            ResizeElement(canvas.Width);
            canvas.SizeChanged += Canvas_SizeChanged;

            AnimateEllpise();
            AnimateRect();
            PrpoertyAnimation();
        }

        void AnimateEllpise()
        {
            SlimCanvas.View.Controls.Animation.Rotate rotate1 = new SlimCanvas.View.Controls.Animation.Rotate(ellipse1, 10);
            rotate1.Start();

            SlimCanvas.View.Controls.Animation.Rotate rotate2 = new SlimCanvas.View.Controls.Animation.Rotate(ellipse2, 10, true);
            rotate2.Start();
        }

        void AnimateRect()
        {
            SlimCanvas.View.Controls.Animation.MoveTo move;
            SlimCanvas.View.Controls.Animation.MoveTo move2;

            Task.Run(async () =>
            {
                for (int i = 0; i < 30; i++)
                {
                    move = new SlimCanvas.View.Controls.Animation.MoveTo(
                        rect, new Vector2(canvas.Width - 50, 0), 2000, SlimCanvas.View.Controls.Animation.AnimateMethode.EaseInCubic);
                    
                    await move.StartAsync();

                    move2 = new SlimCanvas.View.Controls.Animation.MoveTo(
                        rect, new Vector2(0, 0), 1000, SlimCanvas.View.Controls.Animation.AnimateMethode.EaseInSine);

                    await move2.StartAsync();
                }
            });
        }

        void PrpoertyAnimation()
        {
            SlimCanvas.View.Controls.Animation.Animate animate = new SlimCanvas.View.Controls.Animation.Animate(
                path.RotationProperty, 360, 10000, SlimCanvas.View.Controls.Animation.AnimateMethode.LinearTween, true);
            animate.Start();
        }

        private void Canvas_SizeChanged(object sender, SlimCanvas.View.Controls.EventTypes.SizeChangedEventArgs e)
        {
            ResizeElement(e.NewWidth);
        }

        void ResizeElement(double w)
        {
            var scal = w / width;

            ellipse1.Scale = new Vector2(scal, scal);
            ellipse2.Scale = new Vector2(scal, scal);
            path.Scale = new Vector2(scal, scal);
        }
    }
}
