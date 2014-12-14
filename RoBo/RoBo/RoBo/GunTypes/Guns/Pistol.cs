using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class Pistol : PhysicalGun
    {
        public Pistol(Character character)
            : base(character, WeaponType.PISTOL, 0, 4, 0.8f, 0.6f, 20, 230, 64)
        {
        }
    }
}
