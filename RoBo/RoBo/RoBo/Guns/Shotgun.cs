using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoBo
{
    public class Shotgun : Gun
    {
        public Shotgun(Character character)
            : base(character, 5, 0.5f, 0.7f, 2, 230, 64, 6, 10, false)
        {
        }
    }
}
