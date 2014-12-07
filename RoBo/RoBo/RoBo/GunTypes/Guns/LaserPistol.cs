using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class LaserPistol : LaserGun
    {
        public LaserPistol(Character character)
            : base(character, Image.Gun.PhysPistol, 0.035f, 2, 1.0f, 0.5f, 20, 300, 1, 128)
        {
        }
    }
}
