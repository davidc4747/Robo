using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Ammo : Item
    {
        public WeaponType Type
        {
            get;
            private set;
        }

        public Ammo(Vector2 startPos)
            : base(Image.Object.Ammo, 0.037f, startPos)
        {
            Type = (WeaponType)rand.Next(Enum.GetNames(typeof(WeaponType)).Length);
            Quantity = rand.Next((int)(Gun.MaxAmmoBag[(int)Type] * 0.30f + 1.5f));
            Name = "";
            switch (Type)
            {
                case WeaponType.PISTOL:
                    Name += "Pistol ";
                    break;
                case WeaponType.SHOTGUN:
                    Name += "Shotgun ";
                    break;
                case WeaponType.SNIPER:
                    Name += "Sniper ";
                    break;
                case WeaponType.ASSAULT:
                    Name += "Assault ";
                    break;
                case WeaponType.SMG:
                    Name += "SMG ";
                    break;
                case WeaponType.LMG:
                    Name += "LMG ";
                    break;
            }

            Name += "Ammo";
        }
    }
}
