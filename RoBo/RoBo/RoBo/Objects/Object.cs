using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class Object : AnimatedSprite
    {
        protected static Random rand = new Random();

        public bool IsDead
        {
            get;
            protected set;
        }
        
        public Object(Texture2D texture, float scaleFactor, float secondsToCrossScreen, Vector2 startPos)
            : base(texture, scaleFactor, secondsToCrossScreen, startPos)
        {
        }
    }
}
