using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View
{
    public abstract class Brush
    {
    }

    public class SolidColorBrush : Brush
    {
        public Color Color;

        public SolidColorBrush()
        {
            Color = Color.Black;
        }

        public SolidColorBrush(Color color)
        {
            Color = color;
        }
        
        public static SolidColorBrush Black = new SolidColorBrush(Color.Black);
        public static SolidColorBrush Gray = new SolidColorBrush(Color.Gray);
        public static SolidColorBrush White = new SolidColorBrush(Color.White);
        public static SolidColorBrush Brown = new SolidColorBrush(Color.Brown);
        public static SolidColorBrush Red = new SolidColorBrush(Color.Red);
        public static SolidColorBrush Yellow = new SolidColorBrush(Color.Yellow);
        public static SolidColorBrush Green = new SolidColorBrush(Color.Green);
        public static SolidColorBrush Blue = new SolidColorBrush(Color.Blue);
        public static SolidColorBrush Transparent = new SolidColorBrush(Color.Transparent);
    }

    public enum ExtendMode
    {
        Clamp,
        Wrap,
        Mirror
    }

    //public class BitmapBrush : Brush
    //{
    //    public ExtendMode ExtendModeX { get; set; }
    //    public ExtendMode ExtendModeY { get; set; }
    //    public BitmapInterpolationMode InterpolationMode { get; set; }
    //    public Abstractions.IBitmap Bitmap { get; set; }

    //    public BitmapBrush()
    //    {

    //    }

    //    public BitmapBrush(ExtendMode extendModeX, ExtendMode extendModeY, Abstractions.IBitmap bitmap)
    //    {
    //        ExtendModeX = extendModeX;
    //        ExtendModeY = extendModeY;
    //        Bitmap = bitmap;
    //    }
    //}

    public class GradientStop
    {
        public double Position;
        public Color Color;

        public GradientStop()
        {
        }

        public GradientStop(double position, Color color)
        {
            Position = position;
            Color = color;
        }
    }

    public abstract class GradientBrush : Brush
    {
        public List<GradientStop> Stops { get; } = new List<GradientStop>();

        public void AddStop(double position, Color color)
        {
            Stops.Add(new GradientStop(position, color));
        }

        public void AddStopRange(IEnumerable<GradientStop> stops)
        {
            Stops.AddRange(stops);
        }
    }

    public class LinearGradientBrush : GradientBrush
    {
        public Vector2 StartPoint { get; set; }
        public Vector2 EndPoint { get; set; }

        public LinearGradientBrush()
        {
        }

        public LinearGradientBrush(Vector2 startPoint, Vector2 endPoint, IEnumerable<GradientStop> stopsCollection)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Stops.AddRange(stopsCollection);
        }

        public LinearGradientBrush(Vector2 startPoint, Vector2 endPoint, Color startColor, Color endColor)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Stops.Add(new GradientStop(0, startColor));
            Stops.Add(new GradientStop(1, endColor));
        }
    }

    public class RadialGradientBrush : GradientBrush
    {
        public Vector2 Center { get; set; }
        public Vector2 GradientOriginOffset { get; set; }
        public double Radius { get; set; }

        public RadialGradientBrush()
        {

        }
        
        public RadialGradientBrush(Vector2 center, Vector2 gradientOriginOffset, double radius, IEnumerable<GradientStop> stopsCollection)
        {
            Center = center;
            GradientOriginOffset = gradientOriginOffset;
            Radius = radius;
            Stops.AddRange(stopsCollection);
        }

        public RadialGradientBrush(Vector2 center, Vector2 gradientOriginOffset, double radius, Color startColor, Color endColor)
        {
            Center = center;
            GradientOriginOffset = gradientOriginOffset;
            Radius = radius;
            Stops.Add(new GradientStop(0, startColor));
            Stops.Add(new GradientStop(1, endColor));
        }
    }
}
