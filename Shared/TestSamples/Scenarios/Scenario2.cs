using SlimCanvas;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestSamples.Scenarios
{
    public class Scenario2 : ISamples
    {
        SlimCanvas.Canvas canvas;
        SlimCanvas.View.Controls.Primitive.Ellipse ellipse;
        SlimCanvas.View.Controls.Primitive.Rectangle rect;
        SlimCanvas.View.Controls.Primitive.Path path;
        SlimCanvas.View.Controls.Primitive.Line line;

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
                Width = 200,
                Height = 200,
                X = 100,
                Y = 50,
                FillBrush = new SlimCanvas.View.SolidColorBrush(Color.Brown),
                Thickness = 5,
                StrokeColor = SlimCanvas.Color.Chartreuse,
                StrokeStyle = SlimCanvas.View.Controls.EnumTypes.DashStyle.Dot
            };
            canvas.Children.Add(ellipse);

            //Create Rectangle
            rect = new SlimCanvas.View.Controls.Primitive.Rectangle(
                new SlimCanvas.Rect(0, 0, 200, 200),
                SlimCanvas.Color.DarkSlateBlue, 5,
                SlimCanvas.View.Controls.EnumTypes.DashStyle.Solid,
                new SlimCanvas.View.SolidColorBrush(SlimCanvas.Color.DarkSlateGray))
            {
                X = 400,
                Y = 50
            };
            canvas.Children.Add(rect);

            //Create path as triangle
            path = new SlimCanvas.View.Controls.Primitive.Path() {
                X = 700,
                Y = 50
            };
            path.AddPoint(new SlimCanvas.Vector2(100, 0));
            path.AddPoint(new SlimCanvas.Vector2(200, 200));
            path.AddPoint(new SlimCanvas.Vector2(0, 200));
            path.AddPoint(new SlimCanvas.Vector2(100, 0));
            canvas.Children.Add(path);

            //Create underline
            line = new SlimCanvas.View.Controls.Primitive.Line(
                new SlimCanvas.Vector2(0, 260), 
                new SlimCanvas.Vector2(width, 260), 
                SlimCanvas.Color.BlanchedAlmond, 10);
            canvas.Children.Add(line);


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
            path.Scale = new SlimCanvas.Vector2(scal, scal);
            line.Scale = new SlimCanvas.Vector2(scal, scal);
        }
    }
}
