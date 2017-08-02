using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestSamples.Scenarios
{
    public class Scenario1 : ISamples
    {
        SlimCanvas.Canvas canvas;
        
        public void LoadScenario(SlimCanvas.Canvas canvas)
        {
            canvas.Clear();

            this.canvas = canvas;
            canvas.Background = new SlimCanvas.View.SolidColorBrush(new SlimCanvas.Color(47, 47, 47, 255));
            canvas.AutoResize = true;

            string plattform = string.Empty;
            switch (canvas.Platform)
            {
                case SlimCanvas.Plattform.UniversalWindowsDesktop:
                    plattform = "Windows 10 Desktop";
                    break;
                case SlimCanvas.Plattform.UniversalWindowsMobile:
                    plattform = "Windows 10 Mobile";
                    break;
                case SlimCanvas.Plattform.UniversalWindowsTeam:
                    plattform = "Windows 10 Hub";
                    break;
                case SlimCanvas.Plattform.UniversalWindowsIoT:
                    plattform = "Windows 10 IoT";
                    break;
                case SlimCanvas.Plattform.UniversalWindowsXbox:
                    plattform = "Microsoft Xbox";
                    break;
                case SlimCanvas.Plattform.UniversalWindowsHolographic:
                    plattform = "Windows 10 Holographic";
                    break;
                case SlimCanvas.Plattform.Android:
                    plattform = "Android";
                    break;
                case SlimCanvas.Plattform.IOS:
                    plattform = "IOS";
                    break;
                case SlimCanvas.Plattform.MacOS:
                    plattform = "MacOS";
                    break;
            }

            SlimCanvas.View.Controls.TextBlock tbHello = new SlimCanvas.View.Controls.TextBlock()
            {
                Text = $"Hello World.{System.Environment.NewLine}From {plattform}",
                FontSize = 20,
                TextAlignment = SlimCanvas.View.Controls.EnumTypes.TextAlignment.Center,
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center,
                Color = SlimCanvas.Color.White,
                Origin = new SlimCanvas.Vector2(0.5d, 0.5d)
            };
            canvas.Children.Add(tbHello);

            var rotate = new SlimCanvas.View.Controls.Animation.Rotate(tbHello, 45);
            rotate.Start();
        }
    }
}
