using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;
using SlimCanvas.View.Controls.Primitive;
using SlimCanvas.View.Controls;
using CoreText;

namespace SlimCanvas.iOS
{
    internal class DrawLayer : IDisposable
    {
        CGColorSpace cp;
        CGBitmapContext ctx;
        int Width;
        int Height;

        public DrawLayer(int width, int height)
        {
            Width = width;
            Height = height;

            cp = CGColorSpace.CreateDeviceRGB();
            ctx = new CGBitmapContext(IntPtr.Zero, width, height, 8, (int)(width * 4), cp, CGImageAlphaInfo.PremultipliedLast);
        }

        #region Clear

        public void Clear(Color color)
        {
            ctx.SetFillColor(LocalTransform.ToColor(color));
            ctx.FillRect(new CGRect(0, 0, ctx.Width, ctx.Height));
        }

        #endregion

        #region DrawEllipse

        public void DrawEllipse(Ellipse item, Vector2 parentPosition, int elementId)
        {
            ctx.SaveState();
            
            var posInCtx = SetMatrix(item, parentPosition);
            CGRect absolutPos = new CGRect(-posInCtx.X, -posInCtx.Y, item.Width, item.Height);

            ctx.AddEllipseInRect(absolutPos);
            ctx.Clip();
            SetStroke(item);
            SetBrush(item, absolutPos);
            ctx.StrokeEllipseInRect(absolutPos);

            ctx.RestoreState();
        }

        #endregion

        #region DrawRect

        public void DrawRect(Rectangle item, Vector2 parentPosition, int elementId)
        {
            ctx.SaveState();

            var posInCtx = SetMatrix(item, parentPosition);
            CGRect absolutPos = new CGRect(-posInCtx.X, -posInCtx.Y, item.Width, item.Height);

            ctx.AddRect(absolutPos);
            ctx.Clip();
            SetStroke(item);
            SetBrush(item, absolutPos);
            ctx.StrokeRect(absolutPos);

            ctx.RestoreState();
        }

        #endregion

        #region DrawLine

        public void DrawLine(Line item, Vector2 parentPosition, int elementId)
        {
            ctx.SaveState();

            var posInCtx = SetMatrix(item, parentPosition);

            var p = new CGPath();

            p.MoveToPoint(new CGPoint(-posInCtx.X, -posInCtx.Y));
            p.AddLineToPoint(new CGPoint(item.EndPoint.X - item.X - posInCtx.X, item.EndPoint.Y - item.Y - posInCtx.Y));

            ctx.AddPath(p);
            SetStroke(item);
            ctx.StrokePath();

            ctx.RestoreState();
        }

        #endregion

        #region DrawPath

        public void DrawPath(Path item, List<Vector2> pointList, Vector2 parentPosition, int elementId)
        {
            ctx.SaveState();

            var img = LocalCache.GetPath(elementId);
            
            if (img == null)
            {
                using (var c = new DrawInBitmap((int)item.Width, (int)item.Height))
                {
                    c.DrawPath(pointList, item.StrokeColor, item.Thickness, item.StrokeStyle, item.DashPattern);
                    img = c.GetAsImage();
                }

                LocalCache.AddPath(img, elementId);
            }
            
            var posInCtx = SetMatrix(item, new Vector2(parentPosition.X, parentPosition.Y));
            CGRect absolutPos = new CGRect(-posInCtx.X, -posInCtx.Y, img.Width, img.Height);
            ctx.DrawImage(absolutPos, img);

            ctx.RestoreState();
        }

        #endregion

        #region DrawText

        public CGSize DrawText(TextBlock item, Vector2 parentPosition, int elementId)
        {
            if (string.IsNullOrEmpty(item.Text))
                return new CGSize();

            string key = $"{item.Text}x{item.FontSize}x{item.Color.ToString()}x{item.FontStyle.ToString()}x{item.TextAlignment.ToString()}";

            var img = LocalCache.GetText(key);

            if (img == null)
            {
                #region Create Img from text

                var font = GetFont(item.FontStyle, item.FontSize);
                var lines = item.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                List<TextLinesModel> model = new List<TextLinesModel>();

                for (int i = 0; i < lines.Length; i++)
                {
                    var t = lines[i];
                    using (NSString ns = new NSString(t))
                    {
                        var tSize = ns.StringSize(font);
                        model.Insert(0, new TextLinesModel() { Size = tSize, Text = t });
                    }
                }

                var tbMaxWidth = model.Max(m => m.Size.Width);
                var tbHeight = model[0].Size.Height * model.Count;

                var w = (int)Math.Ceiling(tbMaxWidth);
                var h = (int)Math.Ceiling(tbHeight);

                using (var space = CGColorSpace.CreateDeviceRGB())
                {
                    using (var c = new CGBitmapContext(IntPtr.Zero, w, h, 8, w * 4, space, CGImageAlphaInfo.PremultipliedLast))
                    {
                        nfloat tx = 0;
                        nfloat ty = 0;

                        foreach (var row in model)
                        {
                            switch (item.TextAlignment)
                            {
                                case View.Controls.EnumTypes.TextAlignment.Right:
                                    tx = (tbMaxWidth - row.Size.Width);
                                    break;
                                case View.Controls.EnumTypes.TextAlignment.Center:
                                    tx = (tbMaxWidth - row.Size.Width) / 2;
                                    break;
                                default:
                                    tx = 0;
                                    break;
                            }

                            using (var nsText = new NSMutableAttributedString(row.Text))
                            {
                                nsText.AddAttributes(new CTStringAttributes()
                                {
                                    ForegroundColorFromContext = false,
                                    ForegroundColor = LocalTransform.ToColor(item.Color),
                                    Font = new CTFont(font.FamilyName, font.PointSize),

                                }, new NSRange(0, row.Text.Length));

                                using (var l = new CTLine(nsText))
                                {
                                    c.SaveState();

                                    c.TextPosition = new CGPoint(tx, ty);
                                    l.Draw(c);
                                    c.RestoreState();
                                }
                            }

                            ty += row.Size.Height;
                        }

                        img = c.ToImage();
                    }
                }

                #endregion

                LocalCache.AddText(img, key);
            }

            ctx.SaveState();

            var posInCtx = SetMatrix(item, parentPosition);
            CGRect absolutPos = new CGRect(-posInCtx.X, -posInCtx.Y, img.Width, img.Height);
            ctx.InterpolationQuality = CGInterpolationQuality.High;
            
            ctx.DrawImage(absolutPos, img);

            ctx.RestoreState();

            return new CGSize(img.Width, img.Height);
        }

        #region Helper

        class TextLinesModel
        {
            public string Text { get; set; }
            public CGSize Size { get; set; }
        }

        UIFont GetFont(View.Controls.EnumTypes.FontStyle style, double size)
        {
            switch (style)
            {
                case View.Controls.EnumTypes.FontStyle.Italic:
                    return UIFont.FromName("Helvetica-Oblique", (nfloat)size);
                default:
                    return UIFont.FromName("Helvetica", (nfloat)size);
            }
        }

        #endregion

        #endregion

        #region DrawImage

        public void DrawImage(Image item, Vector2 parentPosition, int elementId)
        {
            ctx.SaveState();

            var posInCtx = SetMatrix(item, parentPosition);
            CGRect absolutPos = new CGRect(-posInCtx.X, -posInCtx.Y, item.Width, item.Height);
            ctx.InterpolationQuality = CGInterpolationQuality.High;

            var bitmap = item.Source as IBitmapiOS;

            if (item.Clip != new Rect(0, 0, item.Width, item.Height))
                ctx.ClipToRect(new CGRect(item.Clip.X, item.Clip.Height - item.Clip.Y, item.Clip.Width, item.Clip.Height));

            ctx.DrawImage(absolutPos, bitmap.MyImage);

            ctx.RestoreState();
        }

        #endregion

        #region Helper

        #region SetMatrix

        CGPoint SetMatrix(UIElement item, Vector2 parentPosition)
        {
            // Set coordinate system to top-left

            var origin = new CGPoint(item.Width * item.Origin.X, item.Height * item.Origin.Y);

            var x = (nfloat)(item.ActualX + parentPosition.X + (origin.X * item.Scale.X));
            var y = (nfloat)(Height - (int)item.ActualHeight - parentPosition.Y - (int)item.ActualY + (origin.Y * item.Scale.Y));

            ctx.TranslateCTM(x, y);

            ctx.RotateCTM(-LocalTransform.DegreesToRadians(item.Rotation));
            ctx.ScaleCTM((nfloat)item.Scale.X, (nfloat)item.Scale.Y);

            return origin;
        }

        #endregion

        #region SetStroke

        void SetStroke(BasicPrimitive item)
        {
            if (item.StrokeColor.A > 0 && item.Thickness > 0)
            {
                ctx.SetLineWidth((nfloat)item.Thickness);
                ctx.SetStrokeColor(LocalTransform.ToColor(item.StrokeColor));

                float dot = (float)item.Thickness;
                float dash = dot * 2;

                switch (item.StrokeStyle)
                {
                    case View.Controls.EnumTypes.DashStyle.Dash:
                        ctx.SetLineDash(0, new nfloat[] { dash, dash });
                        break;
                    case View.Controls.EnumTypes.DashStyle.Dot:
                        ctx.SetLineDash(0, new nfloat[] { dot, dot });
                        break;
                    case View.Controls.EnumTypes.DashStyle.DashDotDot:
                        ctx.SetLineDash(0, new nfloat[] { dash, dash, 0, dash });
                        break;
                    case View.Controls.EnumTypes.DashStyle.Custom:
                        var pattern = item.DashPattern.Select(p => (nfloat)p).ToArray();
                        ctx.SetLineDash(0, pattern);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region SetBrush

        void SetBrush(BasicFillPrimitive item, CGRect absolutPos)
        {
            if (item.FillBrush is View.SolidColorBrush c)
            {
                ctx.SetFillColor(LocalTransform.ToColor(c.Color));
                ctx.FillRect(absolutPos);
            }
            else if (item.FillBrush is View.LinearGradientBrush lb)
            {
                var gg = GetGradient(lb.Stops);

                var start = new CGPoint(absolutPos.X, absolutPos.Y + absolutPos.Height);
                var end = new CGPoint(absolutPos.X + absolutPos.Width, absolutPos.Y);
                ctx.DrawLinearGradient(gg, start, end, CGGradientDrawingOptions.None);
            }
            else if (item.FillBrush is View.RadialGradientBrush rb)
            {
                var gg = GetGradient(rb.Stops);

                var centerX = (absolutPos.X + absolutPos.Width) / 2 + rb.GradientOriginOffset.X;
                var centerY = (absolutPos.Y + absolutPos.Height) / 2 + rb.GradientOriginOffset.Y;
                var start = new CGPoint(centerX, centerY);
                ctx.DrawRadialGradient(gg, start, 0, start, (nfloat)rb.Radius, CGGradientDrawingOptions.DrawsAfterEndLocation);
            }
        }

        CGGradient GetGradient(IList<View.GradientStop> stops)
        {
            var c = stops.Count;
            var ls = new nfloat[c];
            var cl = new nfloat[c * 4];

            int cIndex = 0;
            for (int i = 0; i < c; i++)
            {
                var s = stops[i];
                ls[i] = (nfloat)s.Position;
                cl[cIndex] = s.Color.R;
                cl[cIndex + 1] = s.Color.G;
                cl[cIndex + 2] = s.Color.B;
                cl[cIndex + 3] = s.Color.A;

                cIndex += 4;
            }

            var cs = CGColorSpace.CreateDeviceRGB();
            return new CGGradient(cs, cl, ls);
        }

        #endregion

        #endregion

        public CGImage GetAsImage()
        {
            return ctx.ToImage();
        }

        public void Dispose()
        {
            ctx.Dispose();
            cp.Dispose();
        }
    }
}