using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Fonts
    {
        public static SpriteFont Normal
        {
            get { return Game1.GameContent.Load<SpriteFont>("Fonts/Normal"); }
        }

        // other Spritefonts...
    }
}
