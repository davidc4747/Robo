using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class PlasmaShot : Bullet
    {
        public PlasmaShot(Gun gun)
            : base(Image.Bullet.Plasma, 0.03f, 2f, gun)
        {
        }
    }
}
