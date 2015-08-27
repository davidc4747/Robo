using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class GunItem : Item
    {
        public WeaponType WepType
        {
            get;
            private set;
        }

        public TechType TechType
        {
            get;
            private set;
        }

        public GunStats GunShell
        {
            get;
            private set;
        }

        public GunItem(Vector2 startPos, Enemy enemy)
            : base(Image.Object.Ammo, 0.037f, startPos)
        {
            WepType = (WeaponType)rand.Next(Enum.GetNames(typeof(WeaponType)).Length);
            TechType = TechType.HUMAN;//(TechType)rand.Next(Enum.GetNames(typeof(TechType)).Length);

            switch (TechType)
            {
                case TechType.HUMAN:
                    genPhysicalGun(enemy.IQ + rand.Next(-5, 6));
                    break;
                case TechType.ALIEN:
                    genLaserGun();
                    break;
                case TechType.ROBOT:
                    genPlasmaGun();
                    break;
            }

        }

        public void genPhysicalGun(int iq)
        {
            int damage = 0, magSize = 0, numShots = 0, pierce = 0;
            float accuracy = 0, reloadSpd = 0, fireRate = 0, range = 0, DPS = 0;
            bool isAutomatic = false;
            switch (WepType)
            {
                case WeaponType.PISTOL:
                case WeaponType.SHOTGUN:
                case WeaponType.SNIPER:
                case WeaponType.ASSAULT:
                case WeaponType.SMG:
                case WeaponType.LMG:
                    range = 230;
                    DPS = (float)(150 * (iq / 20.0f) * rand.NextDouble() + 12 * iq);    //range * 2?
                    fireRate = (float)(12 * rand.NextDouble() + 5);
                    damage = (int)(DPS / fireRate);                                     //change damage to float??




                    this.changeTexture(Image.Gun.Physical.Pistol, 0.017f);
                    //GunShell = new GunStats(WepType, iq, 10, .8f, .5f, 20, 300, 60, 2, 3);
                    break;
            }

            GunShell = new GunStats(WepType, iq, damage, accuracy, reloadSpd, fireRate, range, magSize, numShots, pierce, isAutomatic);
        }

        public void genLaserGun()
        {
            switch (WepType)
            {
                case WeaponType.PISTOL:
                case WeaponType.SHOTGUN:
                case WeaponType.SNIPER:
                case WeaponType.ASSAULT:
                case WeaponType.SMG:
                case WeaponType.LMG:
                    break;
            }
        }

        public void genPlasmaGun()
        {
            switch (WepType)
            {
                case WeaponType.PISTOL:
                case WeaponType.SHOTGUN:
                case WeaponType.SNIPER:
                case WeaponType.ASSAULT:
                case WeaponType.SMG:
                case WeaponType.LMG:
                    break;
            }
        }
    }
}
