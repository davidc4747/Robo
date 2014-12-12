using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class Shotgun : PhysicalGun
    {
        public Shotgun(Character character)
            : base(character, WeaponType.SHOTGUN, Image.Gun.PhysPistol, 0.025f, 5, 0.5f, 0.7f, 2, 230, 6, 10, 1, false)
        {
        }
    }
}
