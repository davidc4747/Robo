using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class Pistol : Gun
    {
        public Pistol(Character character)
            : base(character, 3, 0.8f, 0.6f, 20, 230, 256, 64)
        {
        }
    }
}
