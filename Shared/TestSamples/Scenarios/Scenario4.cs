using System;
using System.Collections.Generic;
using System.Text;
using SlimCanvas;

namespace TestSamples.Scenarios
{
    public class Scenario4 : ISamples
    {
        Canvas canvas;
        SlimCanvas.View.Controls.Image img;
        SlimCanvas.View.Controls.Image clipImg;
        SlimCanvas.View.Controls.Image editImg;

        double width = 1000;

        public async void LoadScenario(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.Clear();
            canvas.Background = new SlimCanvas.View.SolidColorBrush(new SlimCanvas.Color(47, 47, 47, 255));
            canvas.AutoResize = true;

            using (var logoFile = await canvas.Assets.GetFileFromAssetsAsync("TWyTecLogo600x300.png"))
            {
                var bitmap = await canvas.Graphics.BitmapAsync(logoFile);

                img = new SlimCanvas.View.Controls.Image()
                {
                    Source = bitmap,
                    HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Left,
                    VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Top,
                    Margin = new Margin(20, 20, 0, 0)
                };
                canvas.Children.Add(img);

                clipImg = new SlimCanvas.View.Controls.Image()
                {
                    Source = bitmap,
                    HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Right,
                    VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Top,
                    Clip = new Rect(172, 102, 355, 137),
                    Margin = new Margin(0, 20, 20, 0)
                };
                canvas.Children.Add(clipImg);

                var pixel = bitmap.GetPixels();
                var bitmap2 = await canvas.Graphics.BitmapAsync(pixel, bitmap.Width);

                //Crop
                bitmap2.CropBitmap(new Rect(172, 102, 355, 137));

                //Scale
                var newWidth = bitmap2.Width * 0.5d; //Half size
                var newHeight = bitmap2.Height * 0.5d;
                bitmap2.ScaleBitmap(newWidth, newHeight, BitmapInterpolationMode.Linear);

                editImg = new SlimCanvas.View.Controls.Image()
                {
                    Source = bitmap2,
                    HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                    VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                    Margin = new Margin(0, 0, 0, 20)
                };
                canvas.Children.Add(editImg);
                
            }

            //using (var logoFile2 = await canvas.Assets.GetFileFromAssetsAsync("TWyTecLogo600x300.png"))
            //{
            //    string fileName = $"{Guid.NewGuid().ToString()}.png";

            //    await canvas.Assets.SaveFileToLocalFolderAsync(fileName, logoFile2);
            //    await canvas.Assets.SaveFileToTempAsync(fileName, logoFile2);

            //    using (var myStream = await canvas.Assets.GetFileFromLocalFolderAsync(fileName))
            //    {
            //        canvas.Children.Add(
            //            new SlimCanvas.View.Controls.Image()
            //            {
            //                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Bottom,
            //                Source = await canvas.Graphics.BitmapAsync(myStream)
            //            });
            //    }

            //    using (var myStream = await canvas.Assets.GetFileFromTempAsync(fileName))
            //    {
            //        canvas.Children.Add(
            //            new SlimCanvas.View.Controls.Image()
            //            {
            //                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Bottom,
            //                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Right,
            //                Source = await canvas.Graphics.BitmapAsync(myStream)
            //            });
            //    }
            //}
            
            ResizeElement(canvas.Width);
            canvas.SizeChanged += Canvas_SizeChanged;
        }

        private void Canvas_SizeChanged(object sender, SlimCanvas.View.Controls.EventTypes.SizeChangedEventArgs e)
        {
            ResizeElement(e.NewWidth);
        }

        void ResizeElement(double w)
        {
            var scal = w / width;
            if (scal <= 1)
            {
                img.Scale = new Vector2(scal, scal);
                clipImg.Scale = new Vector2(scal, scal);
                editImg.Scale = new Vector2(scal, scal);
            }
        }
    }
}
