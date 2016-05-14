using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace FillPic
{
    class RecPart
    {
        public float X { get; set; } 
        public float Y { get; set; }

        public Rectangle Bounds;

        public RecPart()
        {
            X = 0;
            Y = 0;
        }
    }
}
