using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View
{
    /// <summary>
    /// Brush
    /// </summary>
    public abstract class Brush
    {
    }

    /// <summary>
    /// SolidColorBrush
    /// </summary>
    public class SolidColorBrush : Brush
    {
        /// <summary>
        /// The Color of this brush
        /// </summary>
        public Color Color;

        /// <summary>
        /// Create with default color black
        /// </summary>
        public SolidColorBrush()
        {
            Color = Color.Black;
        }

        /// <summary>
        /// Create with color
        /// </summary>
        /// <param name="color"></param>
        public SolidColorBrush(Color color)
        {
            Color = color;
        }
    }

    /// <summary>
    /// GradientStop
    /// </summary>
    public class GradientStop
    {
        /// <summary>
        /// Position to stop
        /// </summary>
        public double Position;

        /// <summary>
        /// Color to stop
        /// </summary>
        public Color Color;

        /// <summary>
        /// 
        /// </summary>
        public GradientStop()
        {
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public GradientStop(double position, Color color)
        {
            Position = position;
            Color = color;
        }
    }

    /// <summary>
    /// GradientBrush
    /// </summary>
    public abstract class GradientBrush : Brush
    {
        /// <summary>
        /// Stops
        /// </summary>
        public List<GradientStop> Stops { get; } = new List<GradientStop>();

        /// <summary>
        /// Add stops
        /// </summary>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void AddStop(double position, Color color)
        {
            Stops.Add(new GradientStop(position, color));
        }

        /// <summary>
        /// Add stops range
        /// </summary>
        /// <param name="stops"></param>
        public void AddStopRange(IEnumerable<GradientStop> stops)
        {
            Stops.AddRange(stops);
        }
    }

    /// <summary>
    /// LinearGradientBrush
    /// </summary>
    public class LinearGradientBrush : GradientBrush
    {

        /// <summary>
        /// to be added
        /// </summary>
        public Vector2 StartPoint { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public Vector2 EndPoint { get; set; }


        /// <summary>
        /// to be added
        /// </summary>
        public LinearGradientBrush()
        {
        }

        /// <summary>
        /// to be added
        /// </summary>
        public LinearGradientBrush(Vector2 startPoint, Vector2 endPoint, IEnumerable<GradientStop> stopsCollection)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Stops.AddRange(stopsCollection);
        }

        /// <summary>
        /// to be added
        /// </summary>
        public LinearGradientBrush(Vector2 startPoint, Vector2 endPoint, Color startColor, Color endColor)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Stops.Add(new GradientStop(0, startColor));
            Stops.Add(new GradientStop(1, endColor));
        }
    }

    /// <summary>
    /// to be added
    /// </summary>
    public class RadialGradientBrush : GradientBrush
    {
        /// <summary>
        /// to be added
        /// </summary>
        public Vector2 Center { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public Vector2 GradientOriginOffset { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public double Radius { get; set; }


        /// <summary>
        /// to be added
        /// </summary>
        public RadialGradientBrush()
        {

        }


        /// <summary>
        /// to be added
        /// </summary>
        public RadialGradientBrush(Vector2 center, Vector2 gradientOriginOffset, double radius, IEnumerable<GradientStop> stopsCollection)
        {
            Center = center;
            GradientOriginOffset = gradientOriginOffset;
            Radius = radius;
            Stops.AddRange(stopsCollection);
        }

        /// <summary>
        /// to be added
        /// </summary>
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
