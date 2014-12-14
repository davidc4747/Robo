using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class LaserPistol : LaserGun
    {
        public LaserPistol(Character character)
            : base(character, WeaponType.PISTOL, 0, 3 * 10, 1.0f, 0.5f, 20, 300, 128, 1, 3)
        {
        }
    }
}
