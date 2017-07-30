using System;
using System.Collections.Generic;
using System.Text;
using SlimCanvas;
using System.Threading.Tasks;

namespace TestSamples.Scenarios
{
    public class Scenario6 : ISamples
    {
        //Pointer Event test

        Canvas canvas;
        double width = 1000;

        SlimCanvas.View.Controls.TextBlock tbCords;
        SlimCanvas.View.Controls.Primitive.Rectangle rect;
        SlimCanvas.View.Controls.TextBlock tb;
        Vector2 itemScale;

        public void LoadScenario(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.Clear();
            canvas.Background = new SlimCanvas.View.SolidColorBrush(new Color(47, 47, 47, 255));
            canvas.AutoResize = true;

            tbCords = new SlimCanvas.View.Controls.TextBlock() {
                FontSize = 20,
                Color = Color.White,
                Text = "X"
            };
            canvas.Children.Add(tbCords);

            rect = new SlimCanvas.View.Controls.Primitive.Rectangle()
            {
                Width = 300,
                Height = 300,
                FillBrush = new SlimCanvas.View.SolidColorBrush(Color.White),
                X = 350,
                Y = 100
            };

            tb = new SlimCanvas.View.Controls.TextBlock()
            {
                Text = "Drag me",
                FontSize = 20,
                Color = Color.Black,
                HorizontalAlignment = SlimCanvas.View.Controls.EnumTypes.HorizontalAlignment.Center,
                VerticalAlignment = SlimCanvas.View.Controls.EnumTypes.VerticalAlignment.Center
            };
            rect.Children.Add(tb);

            canvas.Children.Add(rect);
            
            ResizeElement(canvas.Width);
            canvas.SizeChanged += Canvas_SizeChanged;

            canvas.PointerMoved += Canvas_PointerMoved;
            canvas.PointerReleased += Canvas_PointerReleased;

            rect.PointerEntered += Ellipse_PointerEntered;
            rect.PointerExited += Ellipse_PointerExited;
            rect.PointerPressed += Rect_PointerPressed;

            rect.SwipeBottom += Rect_SwipeBottom;
            rect.SwipeTop += Rect_SwipeTop;
            rect.SwipeLeft += Rect_SwipeLeft;
            rect.SwipeRight += Rect_SwipeRight;

            rect.Tapped += Rect_Tapped;
            rect.RightTapped += Rect_RightTapped;
        }
        
        SlimCanvas.View.Controls.UIElement dragItem;
        double relativeX, relativeY;
        bool capured = false;

        #region Moved Rect_PointerPressed Canvas_PointerMoved Canvas_PointerReleased

        private void Rect_PointerPressed(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            if (capured == false)
            {
                capured = true;
                dragItem = sender as SlimCanvas.View.Controls.UIElement;
                relativeX = dragItem.X - (e.X / itemScale.X);
                relativeY = dragItem.Y - (e.Y / itemScale.Y);
            }
        }

        private void Canvas_PointerMoved(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            tbCords.Text = $"X:{(int)e.X} Y:{(int)e.Y}";

            if (capured)
            {
                dragItem.X = (e.X / itemScale.X) + relativeX;
                dragItem.Y = (e.Y / itemScale.Y) + relativeY;
            }
        }

        private void Canvas_PointerReleased(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            e.Handle = true;
            capured = false;
        }

        #endregion

        #region Hover Ellipse_PointerEntered Ellipse_PointerExited

        private void Ellipse_PointerEntered(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            rect.FillBrush = new SlimCanvas.View.SolidColorBrush(Color.Brown);
            tb.Color = Color.White;
        }

        private void Ellipse_PointerExited(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            rect.FillBrush = new SlimCanvas.View.SolidColorBrush(Color.White);
            tb.Color = Color.Black;
        }

        #endregion
        
        #region Swipe

        private async void Rect_SwipeRight(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            var to = new Vector2((canvas.Width - rect.ActualWidth) / itemScale.X, rect.Y);

            var move = new SlimCanvas.View.Controls.Animation.MoveTo(rect, to, 1000, SlimCanvas.View.Controls.Animation.AnimateMethode.LinearTween);
            await move.StartAsync();
        }

        private async void Rect_SwipeLeft(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            var to = new Vector2(0, rect.Y);

            var move = new SlimCanvas.View.Controls.Animation.MoveTo(rect, to, 1000, SlimCanvas.View.Controls.Animation.AnimateMethode.LinearTween);
            await move.StartAsync();
        }

        private async void Rect_SwipeTop(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            var to = new Vector2(rect.X, 0);

            var move = new SlimCanvas.View.Controls.Animation.MoveTo(rect, to, 1000, SlimCanvas.View.Controls.Animation.AnimateMethode.LinearTween);
            await move.StartAsync();
        }

        private async void Rect_SwipeBottom(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            var to = new Vector2(rect.X, (canvas.Height - rect.ActualHeight) / itemScale.Y);

            var move = new SlimCanvas.View.Controls.Animation.MoveTo(rect, to, 1000, SlimCanvas.View.Controls.Animation.AnimateMethode.LinearTween);
            await move.StartAsync();
        }

        #endregion

        #region Tapped and RightTapped

        private void Rect_RightTapped(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            tb.Text = "RightTapped";
        }

        private void Rect_Tapped(object sender, SlimCanvas.View.Controls.EventTypes.PointerRoutedEventArgs e)
        {
            tb.Text = "Tapped";
        }

        #endregion

        #region Canvas_SizeChanged and Scal

        private void Canvas_SizeChanged(object sender, SlimCanvas.View.Controls.EventTypes.SizeChangedEventArgs e)
        {
            ResizeElement(e.NewWidth);
        }

        void ResizeElement(double w)
        {
            var scal = w / width;

            itemScale = new Vector2(scal, scal);

            rect.Scale = itemScale;
            tb.Scale = itemScale;
            tbCords.Scale = itemScale;
        }

        #endregion
    }
}
