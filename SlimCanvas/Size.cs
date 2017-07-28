﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas
{
    public class Size
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Size()
        {

        }

        public Size(double size)
        {
            Width = size;
            Height = size;
        }

        public Size(double width, double height)
        {
            Width = width;
            Height = height;
        }
    }
}
