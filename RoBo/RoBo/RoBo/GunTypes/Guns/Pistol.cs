using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class Pistol : PhysicalGun
    {
        public Pistol(Character character)
            : base(character, Image.Gun.PhysPistol, 0.035f, 4, 0.8f, 0.6f, 20, 230, 256, 64)
        {
        }
    }
}
