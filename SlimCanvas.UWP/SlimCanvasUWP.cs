using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas;
using SlimCanvas.Abstractions;
using SlimCanvas.View.Controls.EventTypes;
using SlimCanvas.View.Controls.EnumTypes;
using SlimCanvas.View;

namespace SlimCanvas.UWP
{
    public class SlimCanvasUWP : Windows.UI.Xaml.Controls.SwapChainPanel, ISlimDraw
    {
        public Canvas SlimCanvasPCL;
        DXSwapDraw swapDraw;
        bool busy = false;

        public SlimCanvasUWP()
        {
            swapDraw = new DXSwapDraw(this);

            var input = new IUserInput(this);

            SlimCanvasPCL = new Canvas(this, input, new IAssetsUwp(), new IGraphicsUwp());

            SetPlattformSettings();
            
            SizeChanged += SlimCanvasUWP_SizeChanged;
            Windows.UI.Xaml.Media.CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        #region SetPlattformSettings

        void SetPlattformSettings()
        {
            var device = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            if (device == "Windows.Mobile")
            {
                SlimCanvasPCL.Plattform = Plattform.UniversalWindowsMobile;
            }
            else if (device == "Windows.Desktop")
            {
                SlimCanvasPCL.Plattform = Plattform.UniversalWindowsDesktop;
            }
            else if (device == "Windows.Xbox")
            {
                SlimCanvasPCL.Plattform = Plattform.UniversalWindowsXbox;
            }
            else if (device == "Windows.Holographic")
            {
                SlimCanvasPCL.Plattform = Plattform.UniversalWindowsHolographic;
            }
            else if (device == "Windows.IoT")
            {
                SlimCanvasPCL.Plattform = Plattform.UniversalWindowsIoT;
            }
            else if (device == "Windows.Team")
            {
                SlimCanvasPCL.Plattform = Plattform.UniversalWindowsTeam;
            }
        }

        #endregion

        #region Events

        private void SlimCanvasUWP_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            if (e.NewSize.IsEmpty)
            {
                return;
            }

            ViewSizeChangedTrigger(e);
        }

        #region DrawUpdate

        public event DrawUpdateEventHandler DrawUpdate;
        protected virtual void OnDrawUpdate(DrawUpdateEventArgs e)
        {
            DrawUpdate?.Invoke(this, e);
        }

        private void CompositionTarget_Rendering(object sender, object e)
        {
            if (busy)
                return;

            DrawUpdateEventArgs args = new DrawUpdateEventArgs();

            if (e is Windows.UI.Xaml.Media.RenderingEventArgs r)
            {
                args.Time = r.RenderingTime;
            }

            OnDrawUpdate(args);
        }

        public void Pause()
        {

        }

        public void Restart()
        {

        }

        #endregion

        #region SizeChanged

        public event SizeChangedEventHandler ViewSizeChanged;
        protected virtual void OnViewSizeChanged(SizeChangedEventArgs e)
        {
            ViewSizeChanged?.Invoke(this, e);
        }
        internal void ViewSizeChangedTrigger(Windows.UI.Xaml.SizeChangedEventArgs e)
        {
            var size = LocalTransform.GetSize(e.NewSize.Width, e.NewSize.Height);

            SizeChangedEventArgs args = new SizeChangedEventArgs()
            {
                NewHeight = (int)size.Height,
                NewWidth = (int)size.Width
            };
            OnViewSizeChanged(args);
        }

        #endregion

        #endregion

        #region Begin/End-Draw

        public void BeginDraw()
        {
            busy = true;
            swapDraw.BeginDraw();
        }

        public void EndDraw()
        {
            swapDraw.EndDraw();
            busy = false;
        }

        #endregion

        #region Clear

        public void Clear(Color color)
        {
            swapDraw.Clear(LocalTransform.ToColor(color));
        }

        #endregion

        #region Draw Primitive

        #region Ellipse

        public void DrawEllipse(SlimCanvas.View.Controls.Primitive.Ellipse ellipse, Vector2 parentPosition, int elementId)
        {
            string key = $"Ellipse{ellipse.Width}x{ellipse.Height}";
            SharpDX.Direct2D1.EllipseGeometry eg = null;

            if (LocalCache.GetGeometry(key) is SharpDX.Direct2D1.EllipseGeometry el)
            {
                eg = el;
            }
            else
            {
                float rX = (float)ellipse.Width / 2;
                float rY = (float)ellipse.Height / 2;
                SharpDX.Direct2D1.Ellipse e = new SharpDX.Direct2D1.Ellipse()
                {
                    Point = new SharpDX.Mathematics.Interop.RawVector2(rX, rY),
                    RadiusX = rX,
                    RadiusY = rY
                };
                eg = new SharpDX.Direct2D1.EllipseGeometry(swapDraw.d2dContext.Factory, e);
                LocalCache.AddGeometry(key, eg);
            }

            swapDraw.DrawGeometry(
                eg,
                GetTransformMartix3x2(ellipse, parentPosition),
                GetStroke(ellipse),
                GetSolidColorBrush(ellipse.StrokeColor, ellipse.Opacity),
                (float)ellipse.Thickness,
                GetBrush(ellipse, elementId)
                );
        }

        #endregion

        #region Rect

        public void DrawRect(SlimCanvas.View.Controls.Primitive.Rectangle rect, Vector2 parentPosition, int elementId)
        {
            string key = $"Rect{rect.Width}x{rect.Height}";
            SharpDX.Direct2D1.RectangleGeometry rg = null;

            if (LocalCache.GetGeometry(key) is SharpDX.Direct2D1.RectangleGeometry r)
            {
                rg = r;
            }
            else
            {
                rg = new SharpDX.Direct2D1.RectangleGeometry(swapDraw.d2dContext.Factory, LocalTransform.ToRectangleF(new Rect(0, 0, rect.Width, rect.Height)));
                LocalCache.AddGeometry(key, rg);
            }
            
            swapDraw.DrawGeometry(
                rg,
                GetTransformMartix3x2(rect, parentPosition),
                GetStroke(rect),
                GetSolidColorBrush(rect.StrokeColor, rect.Opacity),
                (float)rect.Thickness,
                GetBrush(rect, elementId)
                );
        }

        #endregion

        #region Line

        public void DrawLine(SlimCanvas.View.Controls.Primitive.Line line, Vector2 parentPosition, int elementId)
        {
            string key = $"Line{line.X - line.EndPoint.X}x{line.Y - line.EndPoint.Y}";

            SharpDX.Direct2D1.PathGeometry lg = null;

            if (LocalCache.GetGeometry(key) is SharpDX.Direct2D1.PathGeometry l)
            {
                lg = l;
            }
            else
            {
                lg = new SharpDX.Direct2D1.PathGeometry(swapDraw.d2dContext.Factory);

                using (var sink = lg.Open())
                {
                    sink.BeginFigure(LocalTransform.ToVector2(new Vector2(0, 0)), SharpDX.Direct2D1.FigureBegin.Filled);
                    sink.AddLine(LocalTransform.ToVector2(new Vector2(line.EndPoint.X - line.X, line.EndPoint.Y - line.Y)));
                    sink.EndFigure(SharpDX.Direct2D1.FigureEnd.Closed);
                    sink.Close();
                }

                LocalCache.AddGeometry(key, lg);
            }

            swapDraw.DrawGeometry(
                lg,
                GetTransformMartix3x2(line, parentPosition),
                GetStroke(line),
                GetSolidColorBrush(line.StrokeColor, line.Opacity),
                (float)line.Thickness,
                null
                );
        }

        #endregion

        #region Path

        public void DrawPath(SlimCanvas.View.Controls.Primitive.Path path, Vector2 parentPosition, List<Vector2> pointList, int elementId)
        {
            SharpDX.Direct2D1.PathGeometry pg = null;

            if (LocalCache.GetPath(elementId) is SharpDX.Direct2D1.PathGeometry p)
            {
                pg = p;
            }
            else
            {
                pg = new SharpDX.Direct2D1.PathGeometry(swapDraw.d2dContext.Factory);

                using (var sink = pg.Open())
                {
                    for (int i = 0; i < pointList.Count; i++)
                    {
                        var point = pointList[i];

                        if (i == 0)
                            sink.BeginFigure(LocalTransform.ToVector2(point), SharpDX.Direct2D1.FigureBegin.Hollow);
                        else
                            sink.AddLine(LocalTransform.ToVector2(point));

                    }

                    sink.EndFigure(SharpDX.Direct2D1.FigureEnd.Open);
                    sink.Close();
                }

                LocalCache.AddPath(elementId, pg);
            }

            swapDraw.DrawGeometry(
                pg,
                GetTransformMartix3x2(path, parentPosition),
                GetStroke(path),
                GetSolidColorBrush(path.StrokeColor, path.Opacity),
                (float)path.Thickness,
                null
                );
        }

        public void RemoveFromPathCache(int id)
        {
            LocalCache.RemovePath(id);
        }

        #endregion

        #endregion

        #region DrawImage

        public void DrawImage(SlimCanvas.View.Controls.Image img, Vector2 pos, int elementId)
        {
            var b = img.Source as IBitmapUwp;

            swapDraw.DrawBitmap(b.GetBitmap(swapDraw.d2dContext, elementId), GetTransformMartix3x2(img, pos), LocalTransform.ToRectangleF(img.Clip), 1);
        }

        #endregion

        #region DrawText

        public void DrawText(SlimCanvas.View.Controls.TextBlock textBlock, Vector2 pos, int elementId)
        {
            var tb = GetTextLayout(textBlock);

            if (textBlock.Width != tb.Metrics.Width || textBlock.Height != tb.Metrics.Height)
            {
                textBlock.Width = tb.Metrics.Width;
                textBlock.Height = tb.Metrics.Height;
            }


            swapDraw.DrawText(tb, GetSolidColorBrush(textBlock.Color, textBlock.Opacity), GetTransformMartix3x2(textBlock, pos));
        }

        #region Helper

        SharpDX.DirectWrite.TextFormat textFormat;
        SharpDX.DirectWrite.TextLayout textLayout;

        SharpDX.DirectWrite.TextLayout GetTextLayout(SlimCanvas.View.Controls.TextBlock tb)
        {
            if (textFormat != null)
                SharpDX.Utilities.Dispose(ref textFormat);

            if (textLayout != null)
                SharpDX.Utilities.Dispose(ref textLayout);

            textFormat = new SharpDX.DirectWrite.TextFormat(
                Shared.DirectWriteFactory,
                "Segoe UI",
                SharpDX.DirectWrite.FontWeight.Normal,
                GetFontStyle(tb), SharpDX.DirectWrite.FontStretch.Normal, (float)tb.FontSize)
            {
                TextAlignment = GetTextAlignment(tb),
                WordWrapping = SharpDX.DirectWrite.WordWrapping.NoWrap
            };

            textLayout = new SharpDX.DirectWrite.TextLayout(Shared.DirectWriteFactory, tb.Text, textFormat, 100, 100);
            textLayout.MaxWidth = textLayout.Metrics.Width;
            textLayout.MaxHeight = textLayout.Metrics.Height;

            return textLayout;
        }

        //SharpDX.DirectWrite.FontStretch GetFontStretch(SlimCanvas.View.Controls.TextBlock textBlock)
        //{
        //    switch (textBlock.FontStretch)
        //    {
        //        case FontStretch.UltraCondensed:
        //            return SharpDX.DirectWrite.FontStretch.UltraCondensed;
        //        case FontStretch.ExtraCondensed:
        //            return SharpDX.DirectWrite.FontStretch.ExtraCondensed;
        //        case FontStretch.Condensed:
        //            return SharpDX.DirectWrite.FontStretch.Condensed;
        //        case FontStretch.SemiCondensed:
        //            return SharpDX.DirectWrite.FontStretch.SemiCondensed;
        //        case FontStretch.Normal:
        //            return SharpDX.DirectWrite.FontStretch.Normal;
        //        case FontStretch.SemiExpanded:
        //            return SharpDX.DirectWrite.FontStretch.SemiExpanded;
        //        case FontStretch.Expanded:
        //            return SharpDX.DirectWrite.FontStretch.Expanded;
        //        case FontStretch.ExtraExpanded:
        //            return SharpDX.DirectWrite.FontStretch.ExtraExpanded;
        //        case FontStretch.UltraExpanded:
        //            return SharpDX.DirectWrite.FontStretch.UltraExpanded;
        //        default:
        //            return SharpDX.DirectWrite.FontStretch.Undefined;
        //    }
        //}

        SharpDX.DirectWrite.FontStyle GetFontStyle(SlimCanvas.View.Controls.TextBlock textBlock)
        {
            switch (textBlock.FontStyle)
            {
                case FontStyle.Italic:
                    return SharpDX.DirectWrite.FontStyle.Italic;
                default:
                    return SharpDX.DirectWrite.FontStyle.Normal;
            }
        }

        SharpDX.DirectWrite.TextAlignment GetTextAlignment(SlimCanvas.View.Controls.TextBlock textBlock)
        {
            switch (textBlock.TextAlignment)
            {
                case TextAlignment.Right:
                    return SharpDX.DirectWrite.TextAlignment.Trailing;
                case TextAlignment.Center:
                    return SharpDX.DirectWrite.TextAlignment.Center;
                default:
                    return SharpDX.DirectWrite.TextAlignment.Leading;
            }
        }
        

        #endregion

        #endregion

        #region GetStroke

        SharpDX.Direct2D1.StrokeStyle GetStroke(SlimCanvas.View.Controls.Primitive.BasicPrimitive element)
        {
            string key = $"SS{element.StrokeStyle}";

            if (LocalCache.GetStroke(key) is SharpDX.Direct2D1.StrokeStyle s)
            {
                return s;
            }

            SharpDX.Direct2D1.StrokeStyleProperties ssp = new SharpDX.Direct2D1.StrokeStyleProperties();

            float[] dashPattern = new float[0];

            switch (element.StrokeStyle)
            {
                case DashStyle.Solid:
                    ssp.DashStyle = SharpDX.Direct2D1.DashStyle.Solid;
                    break;
                case DashStyle.Dash:
                    ssp.DashStyle = SharpDX.Direct2D1.DashStyle.Dash;
                    break;
                case DashStyle.DashDotDot:
                    ssp.DashStyle = SharpDX.Direct2D1.DashStyle.DashDotDot;
                    break;
                case DashStyle.Dot:
                    ssp.DashStyle = SharpDX.Direct2D1.DashStyle.Custom;
                    dashPattern = new float[2] { 1, 1 };
                    break;
                case DashStyle.Custom:
                    ssp.DashStyle = SharpDX.Direct2D1.DashStyle.Custom;
                    dashPattern = element.DashPattern;
                    break;
            }

            SharpDX.Direct2D1.StrokeStyle ss;

            if (ssp.DashStyle == SharpDX.Direct2D1.DashStyle.Custom)
                ss = new SharpDX.Direct2D1.StrokeStyle(swapDraw.d2dContext.Factory, ssp, dashPattern);
            else
                ss = new SharpDX.Direct2D1.StrokeStyle(swapDraw.d2dContext.Factory, ssp);



            LocalCache.AddStroke(key, ss);

            return ss;
        }

        #endregion

        #region GetSolidColorBrush

        SharpDX.Direct2D1.SolidColorBrush GetSolidColorBrush(SlimCanvas.Color color, double opacity)
        {
            if (color == Color.Transparent)
                return null;

            string key = color.ToString();
            if (LocalCache.GetSolidColor(key) is SharpDX.Direct2D1.SolidColorBrush c)
            {
                c.Opacity = (float)opacity;
                return c;
            }

            var s = new SharpDX.Direct2D1.SolidColorBrush(swapDraw.d2dContext, LocalTransform.ToColor(color));
            s.Opacity = (float)opacity;
            LocalCache.AddSolidColor(key, s);
            return s;
        }

        #endregion

        #region GetBrush

        SharpDX.Direct2D1.Brush GetBrush(SlimCanvas.View.Controls.Primitive.BasicFillPrimitive element, int elementId)
        {
            if (element.FillBrush is SlimCanvas.View.SolidColorBrush c)
            {
                return GetSolidColorBrush(c.Color, element.Opacity);
            }
            else if (element.FillBrush is SlimCanvas.View.LinearGradientBrush lb)
            {
                string key = "lb";
                foreach (var item in lb.Stops)
                {
                    key += $"x{item.Color.ToString()}x{item.Position}";
                }

                SharpDX.Direct2D1.LinearGradientBrush brush;

                if (LocalCache.GetLinearBrush(key) is SharpDX.Direct2D1.LinearGradientBrush l)
                {
                    brush = l;
                }
                else
                {
                    var gsc = ToGradientStopCollection(lb.Stops);

                    brush = new SharpDX.Direct2D1.LinearGradientBrush(
                        swapDraw.d2dContext,
                        new SharpDX.Direct2D1.LinearGradientBrushProperties(),
                        gsc);

                    LocalCache.AddLinearBrush(key, brush);
                }

                brush.EndPoint = LocalTransform.ToVector2(new Vector2(lb.EndPoint.X, lb.EndPoint.Y));
                brush.StartPoint = LocalTransform.ToVector2(new Vector2(lb.StartPoint.X, lb.StartPoint.Y));
                brush.Opacity = (float)element.Opacity;

                return brush;

            }
            else if (element.FillBrush is SlimCanvas.View.RadialGradientBrush rb)
            {

                string key = "rb";
                foreach (var item in rb.Stops)
                {
                    key += $"x{item.Color.ToString()}x{item.Position}";
                }

                SharpDX.Direct2D1.RadialGradientBrush brush;

                if (LocalCache.GetRadialBrush(key) is SharpDX.Direct2D1.RadialGradientBrush r)
                {
                    brush = r;
                }
                else
                {
                    var gsc = ToGradientStopCollection(rb.Stops);
                    brush = new SharpDX.Direct2D1.RadialGradientBrush(
                        swapDraw.d2dContext,
                        new SharpDX.Direct2D1.RadialGradientBrushProperties(),
                        gsc);

                    LocalCache.AddRadialBrush(key, brush);
                }

                brush.Center = LocalTransform.ToVector2(rb.Center);
                brush.GradientOriginOffset = LocalTransform.ToVector2(rb.GradientOriginOffset);
                brush.RadiusX = (float)rb.Radius;
                brush.RadiusY = (float)rb.Radius;
                brush.Opacity = (float)element.Opacity;

                return brush;
            }

            return null;
        }

        SharpDX.Direct2D1.GradientStopCollection ToGradientStopCollection(List<SlimCanvas.View.GradientStop> stops)
        {
            var gs = new SharpDX.Direct2D1.GradientStop[stops.Count];
            for (int i = 0; i < stops.Count; i++)
            {
                var g = stops[i];
                gs[i] = new SharpDX.Direct2D1.GradientStop() { Color = LocalTransform.ToColor(g.Color), Position = (float)g.Position };
            }

            return new SharpDX.Direct2D1.GradientStopCollection(swapDraw.d2dContext, gs);
        }

        #endregion

        #region GetTransformMartix3x2

        SharpDX.Matrix3x2 GetTransformMartix3x2(SlimCanvas.View.Controls.UIElement element, Vector2 pos)
        {
            return LocalTransform.ToMartix3x2(
                new Vector2(element.ActualWidth * element.Origin.X, element.ActualHeight * element.Origin.Y),
                element.Rotation,
                element.Scale,
                pos.X + element.ActualX, pos.Y + element.ActualY);
        }

        #endregion
    }
}
