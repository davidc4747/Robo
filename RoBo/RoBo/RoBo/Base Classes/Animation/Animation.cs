using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Animation
    {
        private Frame[] frames;
        private Texture2D strip;
        public Texture2D Strip
        {
            get { return strip; }
        }

        public int FrameCount
        {
            get { return frames.Length; }
        }
        public int avFrameWidth
        {
            get;
            private set;
        }
        public int avFrameHeight
        {
            get;
            private set;
        }
                
        public Animation(Texture2D strip, int frameWidth, float frameTime)
        {
            this.strip = strip;
            avFrameWidth = frameWidth;
            avFrameHeight = strip.Height;

            frames = new Frame[strip.Width / frameWidth + 1];
            //Set frames
            for (int i = 0; i < strip.Width; i += frameWidth)
                frames[i / frameWidth] = new Frame(new Rectangle(i, 0, frameWidth, strip.Height), frameWidth, frameTime); 
        }

        public Animation(Texture2D strip, Frame[] frames)
        {
            this.strip = strip;
            this.frames = frames;

            int totalfWidth = 0;
            foreach (Frame f in frames)            
                totalfWidth += f.Rec.Width;            
            avFrameWidth = totalfWidth / FrameCount;

            int totalfHeight = 0;
            foreach (Frame f in frames)
                totalfHeight += f.Rec.Height;
            avFrameHeight = totalfHeight / FrameCount;
        }

        public Frame getFrame(int index)
        {
            return frames[index];
        }

        public void setFrame(int index, Frame frame)
        {
            frames[index] = frame;
        }


    }
}
