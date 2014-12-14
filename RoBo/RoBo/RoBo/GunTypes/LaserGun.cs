using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class LaserGun : Gun
    {
        private float reGenTimer = 0;

        private int curAmmo;
        public override int CurAmmo
        {
            get { return curAmmo; }
            protected set
            {
                curAmmo = (value < 0) ? 0 : value;
            }
        }

        private int maxAmmo;
        public override int MaxAmmo
        {
            get { return maxAmmo; }
            protected set
            {
                maxAmmo = (value < 0) ? 0 : value;
            }
        }

        public LaserGun(Character holder, GunStats gunShell)
            : base(holder, gunShell)
        {
        }

        public LaserGun(Character character, WeaponType wepType, int iq, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int MagSize, int numShots = 1, int pierce = 1, bool isAutomatic = true)
            : base(character, wepType, iq, damage, accuracy, reloadSpd, fireRate, range, MagSize, numShots, pierce, isAutomatic)
        {
            muzzleFlare = new RotatingSprite(Image.Muzzle.Laser, 0.05f, 0, Vector2.Zero, this.Rotation);
            muzzleOffset = new Vector2(0, (int)(-muzzleFlare.Rec.Height * 0.28f - this.Rec.Height / 2));

            MaxAmmo = 1;
        }

        protected override void standBy(GameTime gameTime, IStage stage)
        {
            base.standBy(gameTime, stage);
                        
            this.idle(gameTime, stage);
        }

        protected override void idle(GameTime gameTime, IStage stage)//TODO: laser gun builds damage
        {
            base.idle(gameTime, stage);

            //reGenerate ammo after ReloadSpd has passed
            reGenTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (reGenTimer >= ReloadSpd && CurrMag < MagSize)
            {
                CurrMag++;
                reGenTimer = 0;
            }
        }

        protected override void reloading(float reloadTimer)
        {
            isReloading = false;
        }

        protected override Bullet getBullet()
        {
            return new LaserShot(this);
        }
    }
}
