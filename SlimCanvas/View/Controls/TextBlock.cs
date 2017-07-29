using SlimCanvas.View.Controls.EnumTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls
{
    /// <summary>
    /// to be added
    /// </summary>
    public class TextBlock : UIElement
    {
        /// <summary>
        /// to be added
        /// </summary>
        public TextBlock()
        {
            Width = 1;
            Height = 1;
        }

        /// <summary>
        /// to be added
        /// </summary>
        public FontStyle FontStyle { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public TextAlignment TextAlignment { get; set; }

        /// <summary>
        /// to be added
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// to be added
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>
        /// to be added
        /// </summary>
        public double FontSize { get; set; } = 15;
    }
}
