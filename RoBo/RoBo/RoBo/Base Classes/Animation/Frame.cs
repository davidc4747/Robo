using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public struct Frame
    {
        int width;

        Rectangle rec;
        public Rectangle Rec
        {
            get { return rec; }
        }

        public float time;
        public float Time
        {
            get { return time; }
            private set { time = value; }
        }        

        public Frame(Rectangle fRec, int fWidth, float frameTime)
        {
            rec = fRec;            
            width = fWidth;
            time = frameTime;
        }

    }
}
