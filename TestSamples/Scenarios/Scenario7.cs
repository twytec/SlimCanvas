using System;
using System.Collections.Generic;
using System.Text;
using SlimCanvas;

namespace TestSamples.Scenarios
{
    public class Scenario7 : ISamples
    {
        Canvas canvas;
        double width = 3000;
        double height = 1500;

        public async void LoadScenario(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.Clear();
            canvas.AutoResize = false;
            canvas.Width = width;
            canvas.Height = height;

            var img = new SlimCanvas.View.Controls.Image();

            using (var imgStream = await canvas.Assets.GetFileFromAssetsAsync("Img3000x1500.png"))
            {
                var bitmap = await canvas.Graphics.BitmapAsync(imgStream);
                img.Source = bitmap;
            }

            canvas.Children.Add(img);

            canvas.PointerPressed += Canvas_PointerPressed;
            canvas.PointerMoved += Canvas_PointerMoved;
            canvas.PointerReleased += Canvas_PointerReleased;
        }
        
        double lastPosX, lastPosY;
        bool capured = false;

        private void Canvas_PointerPressed(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            if (capured == false)
            {
                capured = true;
                lastPosX = e.X;
                lastPosY = e.Y;
            }
        }

        private void Canvas_PointerMoved(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            if (capured)
            {
                canvas.Camera.X += (lastPosX - e.X);
                canvas.Camera.Y += (lastPosY - e.Y);

                lastPosX = e.X;
                lastPosY = e.Y;
            }
        }

        private void Canvas_PointerReleased(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            e.Handle = true;
            capured = false;
        }
    }
}
