using SlimCanvas.View.Controls.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls
{
    public class TextBlock : UIElement
    {
        public TextBlock()
        {
            Width = 1;
            Height = 1;
        }
        
        public FontStyle FontStyle { get; set; }
        public TextAlignment TextAlignment { get; set; }

        public string Text { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.Black;
        public double FontSize { get; set; } = 15;


    }
}
