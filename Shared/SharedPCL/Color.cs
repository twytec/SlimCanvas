using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    /// <summary>
    /// Color struct
    /// </summary>
    public struct Color
    {
        /// <summary>
        /// Alpha
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// Red
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Green
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Blue
        /// </summary>
        public byte B { get; set; }
        
        /// <summary>
        /// Create new Color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public Color(byte r, byte g, byte b) : this(r, g, b,255)
        {
        }

        /// <summary>
        /// Create new Color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        public Color(byte r, byte g, byte b, byte a)
        {
            A = a;
            B = b;
            R = r;
            G = g;
        }


        /// <summary>
        /// Convert to string
        /// </summary>
        /// <returns>R: {R} G: {G} B: {B} A: {A}</returns>
        public override string ToString()
        {
            return $"R: {R} G: {G} B: {B} A: {A}";
        }

        /// <summary>
        /// to be added
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                return this == ((Color)obj);
            }
            return false;
        }

        /// <summary>
        /// to be added
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return R.GetHashCode() * 5 + G.GetHashCode() * 13 + B.GetHashCode() * 19 + A.GetHashCode() * 29;
        }

        /// <summary>
        /// to be added
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator == (Color a, Color b)
        {
            return a.A == b.A && a.B == b.B && a.G == b.G && a.R == b.R;
        }

        /// <summary>
        /// to be added
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator != (Color a, Color b)
        {
            return a.R != b.R || a.G != b.G || a.B != b.B || a.A != b.A;
        }

        #region Color bank

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color AliceBlue { get; } = new Color(240, 248, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color AntiqueWhite { get; } = new Color(250, 235, 215, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Aqua { get; } = new Color(0, 255, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Aquamarine { get; } = new Color(127, 255, 212, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Azure { get; } = new Color(240, 255, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Beige { get; } = new Color(245, 245, 220, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Bisque { get; } = new Color(255, 228, 196, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Black { get; } = new Color(0, 0, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color BlanchedAlmond { get; } = new Color(255, 235, 205, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Blue { get; } = new Color(0, 0, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color BlueViolet { get; } = new Color(138, 43, 226, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Brown { get; } = new Color(165, 42, 42, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color BurlyWood { get; } = new Color(222, 184, 135, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color CadetBlue { get; } = new Color(95, 158, 160, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Chartreuse { get; } = new Color(127, 255, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Chocolate { get; } = new Color(210, 105, 30, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Coral { get; } = new Color(255, 127, 80, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color CornflowerBlue { get; } = new Color(100, 149, 237, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Cornsilk { get; } = new Color(255, 248, 220, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Crimson { get; } = new Color(220, 20, 60, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Cyan { get; } = new Color(0, 255, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkBlue { get; } = new Color(0, 0, 139, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkCyan { get; } = new Color(0, 139, 139, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkGoldenrod { get; } = new Color(184, 134, 11, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkGray { get; } = new Color(169, 169, 169, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkGreen { get; } = new Color(0, 100, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkKhaki { get; } = new Color(189, 183, 107, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkMagenta { get; } = new Color(139, 0, 139, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkOliveGreen { get; } = new Color(85, 107, 47, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkOrange { get; } = new Color(255, 140, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkOrchid { get; } = new Color(153, 50, 204, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkRed { get; } = new Color(139, 0, 0, 255);
        
        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkSalmon { get; } = new Color(233, 150, 122, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkSeaGreen { get; } = new Color(143, 188, 143, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkSlateBlue { get; } = new Color(72, 61, 139, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkSlateGray { get; } = new Color(47, 79, 79, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkTurquoise { get; } = new Color(0, 206, 209, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DarkViolet { get; } = new Color(148, 0, 211, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DeepPink { get; } = new Color(255, 20, 147, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DeepSkyBlue { get; } = new Color(0, 191, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DimGray { get; } = new Color(105, 105, 105, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color DodgerBlue { get; } = new Color(30, 144, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Firebrick { get; } = new Color(178, 34, 34, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color FloralWhite { get; } = new Color(255, 250, 240, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color ForestGreen { get; } = new Color(34, 139, 34, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Fuchsia { get; } = new Color(255, 0, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Gainsboro { get; } = new Color(220, 220, 220, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color GhostWhite { get; } = new Color(248, 248, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Gold { get; } = new Color(255, 215, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Goldenrod { get; } = new Color(218, 165, 32, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Gray { get; } = new Color(128, 128, 128, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Green { get; } = new Color(0, 128, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color GreenYellow { get; } = new Color(173, 255, 47, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Honeydew { get; } = new Color(240, 255, 240, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color HotPink { get; } = new Color(255, 105, 180, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color IndianRed { get; } = new Color(205, 92, 92, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Indigo { get; } = new Color(75, 0, 130, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Ivory { get; } = new Color(255, 255, 240, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Khaki { get; } = new Color(240, 230, 140, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Lavender { get; } = new Color(230, 230, 250, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LavenderBlush { get; } = new Color(255, 240, 245, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LawnGreen { get; } = new Color(124, 252, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LemonChiffon { get; } = new Color(255, 250, 205, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightBlue { get; } = new Color(173, 216, 230, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightCoral { get; } = new Color(240, 128, 128, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightCyan { get; } = new Color(224, 255, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightGoldenrodYellow { get; } = new Color(250, 250, 210, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightGray { get; } = new Color(211, 211, 211, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightGreen { get; } = new Color(144, 238, 144, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightPink { get; } = new Color(255, 182, 193, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightSalmon { get; } = new Color(255, 160, 122, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightSeaGreen { get; } = new Color(32, 178, 170, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightSkyBlue { get; } = new Color(135, 206, 250, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightSlateGray { get; } = new Color(119, 136, 153, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightSteelBlue { get; } = new Color(176, 196, 222, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LightYellow { get; } = new Color(255, 255, 224, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Lime { get; } = new Color(0, 255, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color LimeGreen { get; } = new Color(50, 205, 50, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Linen { get; } = new Color(250, 240, 230, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Magenta { get; } = new Color(255, 0, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Maroon { get; } = new Color(128, 0, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumAquamarine { get; } = new Color(102, 205, 170, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumBlue { get; } = new Color(0, 0, 205, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumOrchid { get; } = new Color(186, 85, 211, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumPurple { get; } = new Color(147, 112, 219, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumSeaGreen { get; } = new Color(60, 179, 113, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumSlateBlue { get; } = new Color(123, 104, 238, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumSpringGreen { get; } = new Color(0, 250, 154, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumTurquoise { get; } = new Color(72, 209, 204, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MediumVioletRed { get; } = new Color(199, 21, 133, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MidnightBlue { get; } = new Color(25, 25, 112, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MintCream { get; } = new Color(245, 255, 250, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color MistyRose { get; } = new Color(255, 228, 225, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Moccasin { get; } = new Color(255, 228, 181, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color NavajoWhite { get; } = new Color(255, 222, 173, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Navy { get; } = new Color(0, 0, 128, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color OldLace { get; } = new Color(253, 245, 230, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Olive { get; } = new Color(128, 128, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color OliveDrab { get; } = new Color(107, 142, 35, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Orange { get; } = new Color(255, 165, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color OrangeRed { get; } = new Color(255, 69, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Orchid { get; } = new Color(218, 112, 214, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PaleGoldenrod { get; } = new Color(238, 232, 170, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PaleGreen { get; } = new Color(152, 251, 152, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PaleTurquoise { get; } = new Color(175, 238, 238, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PaleVioletRed { get; } = new Color(219, 112, 147, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PapayaWhip { get; } = new Color(255, 239, 213, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PeachPuff { get; } = new Color(255, 218, 185, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Peru { get; } = new Color(205, 133, 63, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Pink { get; } = new Color(255, 192, 203, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Plum { get; } = new Color(221, 160, 221, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color PowderBlue { get; } = new Color(176, 224, 230, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Purple { get; } = new Color(128, 0, 128, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Red { get; } = new Color(255, 0, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color RosyBrown { get; } = new Color(188, 143, 143, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color RoyalBlue { get; } = new Color(65, 105, 225, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SaddleBrown { get; } = new Color(139, 69, 19, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Salmon { get; } = new Color(250, 128, 114, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SandyBrown { get; } = new Color(244, 164, 96, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SeaGreen { get; } = new Color(46, 139, 87, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SeaShell { get; } = new Color(255, 245, 238, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Sienna { get; } = new Color(160, 82, 45, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Silver { get; } = new Color(192, 192, 192, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SkyBlue { get; } = new Color(135, 206, 235, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SlateBlue { get; } = new Color(106, 90, 205, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SlateGray { get; } = new Color(112, 128, 144, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Snow { get; } = new Color(255, 250, 250, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SpringGreen { get; } = new Color(0, 255, 127, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color SteelBlue { get; } = new Color(70, 130, 180, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Tan { get; } = new Color(210, 180, 140, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Teal { get; } = new Color(0, 128, 128, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Thistle { get; } = new Color(216, 191, 216, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Tomato { get; } = new Color(255, 99, 71, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Transparent { get; } = new Color(255, 255, 255, 0);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Turquoise { get; } = new Color(64, 224, 208, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Violet { get; } = new Color(238, 130, 238, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Wheat { get; } = new Color(245, 222, 179, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color White { get; } = new Color(255, 255, 255, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color WhiteSmoke { get; } = new Color(245, 245, 245, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color Yellow { get; } = new Color(255, 255, 0, 255);

        /// <summary>
        /// Get this color
        /// </summary>
        public static Color YellowGreen { get; } = new Color(154, 205, 50, 255);


        #endregion
    }
}
