using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Animation
{
    public enum AnimateMethode
    {
        LinearTween,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc
    }

    internal class AnimateInternal
    {
        public static double GetValue(double time, double startValue, double endValue, double duration, AnimateMethode met)
        {
            switch (met)
            {
                case AnimateMethode.LinearTween:
                    return LinearTween(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutQuad:
                    return EaseOutQuad(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutQuad:
                    return EaseInOutQuad(time, startValue, endValue, duration);
                case AnimateMethode.EaseInCubic:
                    return EaseInCubic(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutCubic:
                    return EaseOutCubic(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutCubic:
                    return EaseInOutCubic(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutQuart:
                    return EaseOutQuart(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutQuart:
                    return EaseInOutQuart(time, startValue, endValue, duration);
                case AnimateMethode.EaseInQuint:
                    return EaseInQuint(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutQuint:
                    return EaseOutQuint(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutQuint:
                    return EaseInOutQuint(time, startValue, endValue, duration);
                case AnimateMethode.EaseInSine:
                    return EaseInSine(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutSine:
                    return EaseOutSine(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutSine:
                    return EaseInOutSine(time, startValue, endValue, duration);
                case AnimateMethode.EaseInExpo:
                    return EaseInExpo(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutExpo:
                    return EaseOutExpo(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutExpo:
                    return EaseInOutExpo(time, startValue, endValue, duration);
                case AnimateMethode.EaseInCirc:
                    return EaseInCirc(time, startValue, endValue, duration);
                case AnimateMethode.EaseOutCirc:
                    return EaseOutCirc(time, startValue, endValue, duration);
                case AnimateMethode.EaseInOutCirc:
                    return EaseInOutCirc(time, startValue, endValue, duration);
                default:
                    return 0;
            }
        }
        
        static double LinearTween(double t, double b, double c, double d)
        {
            t /= d;
            return c * t * t + b;
        }

        static double EaseOutQuad(double t, double b, double c, double d)
        {
            t /= d;
            return -c * t * (t - 2) + b;
        }

        static double EaseInOutQuad(double t, double b, double c, double d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t + b;
            t--;
            return -c / 2 * (t * (t - 2) - 1) + b;
        }

        static double EaseInCubic(double t, double b, double c, double d)
        {
            t /= d;
            return c * t * t * t + b;
        }

        static double EaseOutCubic(double t, double b, double c, double d)
        {
            t /= d;
            t--;
            return c * (t * t * t + 1) + b;
        }

        static double EaseInOutCubic(double t, double b, double c, double d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t + b;
            t -= 2;
            return c / 2 * (t * t * t + 2) + b;
        }

        static double EaseOutQuart(double t, double b, double c, double d)
        {
            t /= d;
            return c * t * t * t * t + b;
        }

        static double EaseInOutQuart(double t, double b, double c, double d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t + b;
            t -= 2;
            return -c / 2 * (t * t * t * t - 2) + b;
        }

        static double EaseInQuint(double t, double b, double c, double d)
        {
            t /= d;
            return c * t * t * t * t * t + b;
        }

        static double EaseOutQuint(double t, double b, double c, double d)
        {
            t /= d;
            t--;
            return c * (t * t * t * t * t + 1) + b;
        }

        static double EaseInOutQuint(double t, double b, double c, double d)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * t * t * t * t * t + b;
            t -= 2;
            return c / 2 * (t * t * t * t * t + 2) + b;
        }

        static double EaseInSine(double t, double b, double c, double d)
        {
            return (double)(-c * Math.Cos(t / d * (Math.PI / 2)) + c + b);
        }

        static double EaseOutSine(double t, double b, double c, double d)
        {
            return (double)(c * Math.Sin(t / d * (Math.PI / 2)) + b);
        }

        static double EaseInOutSine(double t, double b, double c, double d)
        {
            return (double)(-c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b);
        }

        static double EaseInExpo(double t, double b, double c, double d)
        {
            return (double)(c * Math.Pow(2, 10 * (t / d - 1)) + b);
        }

        static double EaseOutExpo(double t, double b, double c, double d)
        {
            return (double)(c * (-Math.Pow(2, -10 * t / d) + 1) + b);
        }

        static double EaseInOutExpo(double t, double b, double c, double d)
        {
            t /= d / 2;
            if (t < 1) return (double)(c / 2 * Math.Pow(2, 10 * (t - 1)) + b);
            t--;
            return (double)(c / 2 * (-Math.Pow(2, -10 * t) + 2) + b);
        }

        static double EaseInCirc(double t, double b, double c, double d)
        {
            t /= d;
            return (double)(-c * (Math.Sqrt(1 - t * t) - 1) + b);
        }

        static double EaseOutCirc(double t, double b, double c, double d)
        {
            t /= d;
            t--;
            return (double)(c * Math.Sqrt(1 - t * t) + b);
        }

        static double EaseInOutCirc(double t, double b, double c, double d)
        {
            t /= d / 2;
            if (t < 1) return (double)(-c / 2 * (Math.Sqrt(1 - t * t) - 1) + b);
            t -= 2;
            return (double)(c / 2 * (Math.Sqrt(1 - t * t) + 1) + b);
        }
    }
}
