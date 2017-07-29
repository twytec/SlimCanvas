using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SlimCanvas.View.Controls.EventTypes;
using System.Threading.Tasks;
using SlimCanvas.View.Controls.Primitive;
using SlimCanvas.View.Controls;
using Android.Graphics;

namespace SlimCanvas.Droid
{
    internal class DrawInCanvas : Abstractions.ISlimDraw
    {
        Context context;
        public bool runThread = false;
        public ISurfaceHolder holder;
        Android.Graphics.Canvas canvas;
        SurfaceView sv;

        public DrawInCanvas(SurfaceView sv, Context context)
        {
            this.sv = sv;
            this.context = context;
        }

        #region Event

        public event SizeChangedEventHandler ViewSizeChanged;
        protected virtual void OnViewSizeChanged(SizeChangedEventArgs e)
        {
            ViewSizeChanged?.Invoke(this, e);
        }
        internal void ViewSizeChangedTrigger(int width, int height)
        {
            var m = context.Resources.DisplayMetrics;
            SizeChangedEventArgs args = new SizeChangedEventArgs()
            {
                NewHeight = height,
                NewWidth = width
            };
            OnViewSizeChanged(args);
        }

        #region Update

        System.Diagnostics.Stopwatch stopwatch;
        public event DrawUpdateEventHandler DrawUpdate;
        protected virtual void OnDrawUpdate(DrawUpdateEventArgs e)
        {
            DrawUpdate?.Invoke(this, e);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public void Run()
        {
            runThread = true;
            long lastUpdate = 0;
            stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            DrawUpdateEventArgs e = new DrawUpdateEventArgs();

            while (runThread)
            {
                if (System.Environment.TickCount - lastUpdate > 50)
                {
                    lastUpdate = System.Environment.TickCount;
                    e.Time = stopwatch.Elapsed;
                    OnDrawUpdate(e);
                }
            }

            runThread = false;
        }

        public void Pause()
        {
            runThread = false;
            stopwatch.Stop();
        }

        public void Restart()
        {
            if (runThread == false && holder != null)
                Task.Run(() => Run());
        }

        #endregion

        #endregion

        #region BeginDraw EndDraw

        public void BeginDraw()
        {
            if (holder.Surface.IsValid)
                canvas = holder.LockCanvas();
        }

        public void EndDraw()
        {
            if (!CanDraw())
                return;

            holder.UnlockCanvasAndPost(canvas);
            canvas = null;
        }

        #endregion

        #region CanDraw

        bool CanDraw()
        {
            if (canvas == null)
                return false;

            return true;
        }

        #endregion

        #region Clear

        public void Clear(Color color)
        {
            if (!CanDraw())
                return;

            canvas.DrawColor(LocalTransform.ToColor(color));
        }

        #endregion

        #region Draw Primitive

        #region DrawEllipse

        public void DrawEllipse(Ellipse ellipse, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            canvas.Save();
            canvas.Concat(GetMatrix(ellipse, parentPosition));

            if (GetDrawFill(ellipse))
                canvas.DrawOval(LocalTransform.ToRectF(0, 0, ellipse.Width, ellipse.Height), GetBrush(ellipse));
            if (GetDrawStroke(ellipse))
                canvas.DrawOval(LocalTransform.ToRectF(0, 0, ellipse.Width, ellipse.Height), GetStroke(ellipse));

            canvas.Restore();
        }

        #endregion

        #region DrawRect

        public void DrawRect(Rectangle rect, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            canvas.Save();
            canvas.Concat(GetMatrix(rect, parentPosition));

            if (GetDrawFill(rect))
                canvas.DrawRect(LocalTransform.ToRect(0, 0, rect.Width, rect.Height), GetBrush(rect));
            if (GetDrawStroke(rect))
                canvas.DrawRect(LocalTransform.ToRect(0, 0, rect.Width, rect.Height), GetStroke(rect));

            canvas.Restore();
        }

        #endregion

        #region DrawLine

        public void DrawLine(Line line, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            canvas.Save();
            canvas.Concat(GetMatrix(line, parentPosition));
            canvas.DrawLine(
                0,
                0,
                (float)(line.EndPoint.X - line.X),
                (float)(line.EndPoint.Y - line.Y),
                GetStroke(line));
            canvas.Restore();
        }

        #endregion

        #region DrawPath

        public void DrawPath(SlimCanvas.View.Controls.Primitive.Path path, Vector2 parentPosition, List<Vector2> pointList, int elementId)
        {
            if (!CanDraw())
                return;

            canvas.Save();
            canvas.Concat(GetMatrix(path, parentPosition));

            var p = new Android.Graphics.Path();
            for (int i = 0; i < pointList.Count; i++)
            {
                var point = pointList[i];

                if (i == 0)
                    p.MoveTo((float)point.X, (float)(point.Y));
                else
                    p.LineTo((float)point.X, (float)point.Y);
            }

            canvas.DrawPath(p, GetStroke(path));
            canvas.Restore();
        }

        #endregion

        #endregion

        #region DrawText

        public void DrawText(TextBlock tb, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            canvas.Save();


            Android.Text.TextPaint text = new Android.Text.TextPaint(PaintFlags.AntiAlias);
            text.SetTypeface(GetFontStyle(tb));
            text.TextSize = (float)tb.FontSize;
            text.SetARGB(tb.Color.A, tb.Color.R, tb.Color.G, tb.Color.B);
            text.Alpha = (int)(tb.Opacity * 255);

            var lines = tb.Text.Split(new string[1] { System.Environment.NewLine }, StringSplitOptions.None);
            var h = text.FontSpacing * lines.Count();
            var w = (int)text.MeasureText(tb.Text);

            if (h != tb.Height)
                tb.Height = h;
            if (w != tb.Width)
                tb.Width = w;

            canvas.Concat(GetMatrix(tb, parentPosition));

            Android.Text.StaticLayout layout = new Android.Text.StaticLayout(
                tb.Text, text, w, GetTextAlignment(tb), 1.0f, 0, false);

            layout.Draw(canvas);

            canvas.Restore();
        }

        #region Helper

        Typeface GetFontStyle(TextBlock text)
        {
            switch (text.FontStyle)
            {
                case View.Controls.EnumTypes.FontStyle.Italic:
                    return Typeface.Create("Georgia", TypefaceStyle.Italic);
                default:
                    return Typeface.Create("Georgia", TypefaceStyle.Normal);
            }
        }

        Android.Text.Layout.Alignment GetTextAlignment(TextBlock text)
        {
            switch (text.TextAlignment)
            {
                case View.Controls.EnumTypes.TextAlignment.Right:
                    return Android.Text.Layout.Alignment.AlignOpposite;
                case View.Controls.EnumTypes.TextAlignment.Center:
                    return Android.Text.Layout.Alignment.AlignCenter;
                default:
                    return Android.Text.Layout.Alignment.AlignNormal;
            }
        }


        #endregion

        #endregion

        #region DrawImage

        public void DrawImage(Image img, Vector2 parentPosition, int elementId)
        {
            if (!CanDraw())
                return;

            if (img.Source is IBitmapDroid b)
            {
                canvas.Save();
                canvas.Concat(GetMatrix(img, parentPosition));

                var paint = new Paint()
                {
                    Alpha = (int)(img.Opacity * 255)
                };

                canvas.ClipRect(LocalTransform.ToRect(img.Clip), Region.Op.Intersect);

                if (b.myBitmap != null)
                {
                    canvas.DrawBitmap(b.myBitmap,
                        LocalTransform.ToRect(0, 0, img.Width, img.Height),
                        LocalTransform.ToRect(0, 0, img.Width, img.Height),
                        paint);
                }

                canvas.Restore();
            }
        }

        #endregion

        #region GetStroke

        Paint GetStroke(BasicPrimitive element)
        {
            var paint = new Paint(PaintFlags.AntiAlias);
            paint.SetStyle(Paint.Style.Stroke);
            paint.SetARGB(element.StrokeColor.A, element.StrokeColor.R, element.StrokeColor.G, element.StrokeColor.B);
            paint.StrokeWidth = (float)element.Thickness;
            paint.Alpha = (int)(element.Opacity * 255);

            float dot = (float)element.Thickness;
            float dash = dot * 2;

            switch (element.StrokeStyle)
            {
                case View.Controls.EnumTypes.DashStyle.Dash:
                    paint.SetPathEffect(new DashPathEffect(new float[] { dash, dash }, 0));
                    break;
                case View.Controls.EnumTypes.DashStyle.DashDotDot:
                    paint.SetPathEffect(new DashPathEffect(new float[] { dash, dash, 0, dash }, 0));
                    break;
                case View.Controls.EnumTypes.DashStyle.Dot:
                    paint.SetPathEffect(new DashPathEffect(new float[] { dot, dot }, 0));
                    break;
                case View.Controls.EnumTypes.DashStyle.Custom:
                    paint.SetPathEffect(new DashPathEffect(element.DashPattern, 0));
                    break;
                default:
                    break;
            }

            return paint;
        }

        #endregion

        #region GetBrush

        Paint GetBrush(BasicFillPrimitive ele)
        {
            var p = new Paint(PaintFlags.AntiAlias);
            p.SetStyle(Paint.Style.Fill);
            p.Alpha = (int)(ele.Opacity * 255);

            if (ele.FillBrush is View.SolidColorBrush s)
            {
                p.SetARGB(s.Color.A, s.Color.R, s.Color.G, s.Color.B);
            }
            else if (ele.FillBrush is View.LinearGradientBrush l)
            {
                var sC = l.Stops.Count;

                if (sC >= 2)
                {
                    int[] colors = new int[sC];
                    float[] pos = new float[sC];

                    for (int i = 0; i < sC; i++)
                    {
                        var stop = l.Stops[i];
                        pos[i] = (float)(stop.Position * ele.Width);
                        colors[i] = LocalTransform.ToIntColorArgb(stop.Color);
                    }

                    var lg = new LinearGradient(
                        (float)l.StartPoint.X, (float)l.StartPoint.Y,
                        (float)l.EndPoint.X, (float)l.EndPoint.Y,
                        colors, pos, Shader.TileMode.Clamp);

                    p.SetShader(lg);
                }
            }
            else if (ele.FillBrush is View.RadialGradientBrush r)
            {
                var sC = r.Stops.Count;

                if (sC >= 2)
                {
                    int[] colors = new int[sC];
                    float[] pos = new float[sC];

                    for (int i = 0; i < sC; i++)
                    {
                        var stop = r.Stops[i];
                        pos[i] = (float)(stop.Position * (ele.Width));
                        colors[i] = LocalTransform.ToIntColorArgb(stop.Color);
                    }
                    var center = new Vector2(ele.Width / 2 + r.GradientOriginOffset.X, ele.Height / 2 + r.GradientOriginOffset.Y);
                    var lg = new RadialGradient(
                        (float)center.X, (float)center.Y,
                        (float)r.Radius,
                        colors, pos, Shader.TileMode.Clamp);

                    p.SetShader(lg);
                }
            }

            return p;
        }

        #endregion

        #region GetMatrix

        Matrix GetMatrix(UIElement element, Vector2 parentPosition)
        {
            return LocalTransform.GetMatrix(
                element.Rotation,
                new Vector2(element.Width * element.Origin.X, element.Height * element.Origin.Y),
                element.Scale,
                new Vector2(element.ActualX + parentPosition.X, element.ActualY + parentPosition.Y));
        }

        #endregion

        #region GetDrawFill

        bool GetDrawFill(View.Controls.Primitive.BasicFillPrimitive p)
        {
            if (p.FillBrush is View.SolidColorBrush s)
            {
                if (s.Color == Color.Transparent)
                    return false;
            }

            return true;
        }

        #endregion

        #region GetDrawStroke

        bool GetDrawStroke(BasicPrimitive p)
        {
            if (p.StrokeColor == Color.Transparent)
                return false;

            if (p is BasicFillPrimitive pp)
            {
                if (pp.FillBrush is View.SolidColorBrush s)
                {
                    if (s.Color == p.StrokeColor)
                        return false;
                }
            }

            return true;
        }

        #endregion

        public void RemoveFromPathCache(int id)
        {

        }
    }
}