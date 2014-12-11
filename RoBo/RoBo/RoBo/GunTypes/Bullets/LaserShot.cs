using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class LaserShot : Bullet
    {
        public LaserShot(Gun gun)
            : base(Image.Bullet.Laser, 0.02f, .7f, gun)
        {
        }        
    }
}
