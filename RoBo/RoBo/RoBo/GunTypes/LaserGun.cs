using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public abstract class LaserGun : Gun
    {
        private float reGenTimer = 0;

        public LaserGun(Character character, Texture2D texture, float scaleFactor, int damage, float accuracy, float reloadSpd, float fireRate, float range,
            int ammoCap, int MagSize, int numShots = 1, bool isAutomatic = true, bool isSmallArms = true)
            : base(character, texture, scaleFactor, damage, accuracy, reloadSpd, fireRate, range, ammoCap, MagSize, numShots, isAutomatic, isSmallArms)
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

        protected override void idle(GameTime gameTime, IStage stage)
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
