using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Zombie : Enemy
    {
        public Zombie(Vector2 startPos)
            : base(Image.Enemy.Zombie, 0.11f, 3f, startPos, 200)
        {
        }
    }
}
