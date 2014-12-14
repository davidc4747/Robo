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
            switch (WepType)
            {
                case WeaponType.PISTOL:
                case WeaponType.SHOTGUN:
                case WeaponType.SNIPER:
                case WeaponType.ASSAULT:
                case WeaponType.SMG:
                case WeaponType.LMG:
                    this.changeTexture(Image.Gun.Physical.Pistol, 0.017f);
                    GunShell = new GunStats(WepType, iq, 10, .8f, .5f, 20, 300, 60, 2, 3);
                    break;
            }
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
