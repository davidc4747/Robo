using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class PlasmaPistol : PlasmaGun
    {
        public PlasmaPistol(Character character)
            : base(character, WeaponType.PISTOL, Image.Gun.PhysPistol, 0.019f, 8, 0.78f, 0.64f, 17, 170, 64)
        {
        }
    }
}
