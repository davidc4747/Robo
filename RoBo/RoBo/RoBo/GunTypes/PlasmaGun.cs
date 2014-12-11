using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class PlasmaGun : Gun
    {
        public PlasmaGun(Character character, Texture2D texture, float scaleFactor, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int ammoCap, int MagSize, int numShots = 1, int pierce = 1, bool isAutomatic = true, bool isSmallArms = true)
            : base(character, texture, scaleFactor, damage, accuracy, reloadSpd, fireRate, range, ammoCap, MagSize, numShots, pierce, isAutomatic, isSmallArms)
        {
            muzzleFlare = new RotatingSprite(Image.Muzzle.Plasma, 0.05f, 0, Vector2.Zero, this.Rotation);
            muzzleOffset = new Vector2(0, (int)(-muzzleFlare.Rec.Height * 0.28f - this.Rec.Height / 2));
        }

        protected override Bullet getBullet()
        {
            return new PlasmaShot(this);
        }
    }
}
