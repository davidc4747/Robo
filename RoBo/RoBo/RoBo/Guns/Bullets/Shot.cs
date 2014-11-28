using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Shot : Bullet
    {
        public Shot(Gun gun)
            : base(Image.Bullet.Normal, 0.01f, 1, gun)
        {
        }
    }
}
