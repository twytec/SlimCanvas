using System;
using System.Collections.Generic;
using System.Text;

namespace TestSamples.Scenarios
{
    public class Scenario3 : ISamples
    {
        SlimCanvas.Canvas canvas;
        SlimCanvas.View.Controls.Primitive.Ellipse ellipse;
        SlimCanvas.View.Controls.Primitive.Rectangle rect;
        SlimCanvas.View.Controls.TextBlock tbEllipse;
        SlimCanvas.View.Controls.TextBlock tbRect;

        double width = 1000;

        public void LoadScenario(SlimCanvas.Canvas canvas)
        {
            canvas.Clear();

            this.canvas = canvas;
            canvas.Background = new SlimCanvas.View.SolidColorBrush(new SlimCanvas.Color(47, 47, 47, 255));
            canvas.AutoResize = true;

            //Create Ellipse
            ellipse = new SlimCanvas.View.Controls.Primitive.Ellipse()
            {
                Width = 350,
                Height = 350,
                X = 100,
                Y = 100,
                FillBrush =
                new SlimCanvas.View.RadialGradientBrush(
                    new SlimCanvas.Vector2(175, 175),
                    new SlimCanvas.Vector2(0, 0),
                    250,
                    SlimCanvas.Color.Blue, SlimCanvas.Color.DarkMagenta),
                Thickness = 1,
                StrokeColor = SlimCanvas.Color.Black,
                StrokeStyle = SlimCanvas.View.Controls.EnumTypes.DashStyle.Solid
            };

            tbEllipse = new SlimCanvas.View.Controls.TextBlock()
            {
                Text = "RadialGradientBrush",
                FontSize = 20,
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                Color = SlimCanvas.Color.Gainsboro
            };
            ellipse.Children.Add(tbEllipse);
            canvas.Children.Add(ellipse);

            //Create Rectangle
            rect = new SlimCanvas.View.Controls.Primitive.Rectangle(
                new SlimCanvas.Rect(0, 0, 350, 350),
                SlimCanvas.Color.Black, 1,
                SlimCanvas.View.Controls.EnumTypes.DashStyle.Solid)
            {
                X = 550,
                Y = 100
            };
            rect.FillBrush = new SlimCanvas.View.LinearGradientBrush(
                    new SlimCanvas.Vector2(0, 0), new SlimCanvas.Vector2(300, 300), SlimCanvas.Color.White, SlimCanvas.Color.Black);

            tbRect = new SlimCanvas.View.Controls.TextBlock() {
                Text = "LinearGradientBrush",
                FontSize = 20,
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                Color = SlimCanvas.Color.Gainsboro
            };
            rect.Children.Add(tbRect);

            canvas.Children.Add(rect);
            
            canvas.SizeChanged += Canvas_SizeChanged;
            ResizeElement(canvas.Width);
        }

        private void Canvas_SizeChanged(object sender, SlimCanvas.View.Controls.EventTypes.SizeChangedEventArgs e)
        {
            ResizeElement(e.NewWidth);
        }

        void ResizeElement(double w)
        {
            var scal = w / width;
            ellipse.Scale = new SlimCanvas.Vector2(scal, scal);
            rect.Scale = new SlimCanvas.Vector2(scal, scal);
            tbEllipse.Scale = new SlimCanvas.Vector2(scal, scal);
            tbRect.Scale = new SlimCanvas.Vector2(scal, scal);
        }
    }
}
