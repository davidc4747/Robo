using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Stage1 : Stage
    {
        public Stage1()
            : base()
        {
            enemies.Add(new Zombie(new Vector2(300, 100)));
        }

    }
}
