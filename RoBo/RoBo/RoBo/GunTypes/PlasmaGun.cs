using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class PlasmaGun : Gun
    {
        public PlasmaGun(Character holder, GunStats gunShell)
            : base(holder, gunShell)
        {
        }

        public PlasmaGun(Character character, WeaponType wepType, int iq, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int MagSize, int numShots = 1, int pierce = 1, bool isAutomatic = true)
            : base(character, wepType, iq, damage, accuracy, reloadSpd, fireRate, range, MagSize, numShots, pierce, isAutomatic)
        {
            muzzleFlare = new RotatingSprite(Image.Muzzle.Plasma, 0.05f, 0, Vector2.Zero, this.Rotation);
            muzzleOffset = new Vector2(0, (int)(-muzzleFlare.Rec.Height * 0.28f - this.Rec.Height / 2));

            color = Color.GreenYellow;
        }

        protected override Bullet getBullet()
        {
            return new PlasmaShot(this);
        }
    }
}
