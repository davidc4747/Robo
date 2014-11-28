using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RoBo
{
    public class Inventory
    {
        Character character;

        public List<Gun> Guns
        {
            get;
            private set;
        }

        public int HumSalvage
        {
            get;
            private set;
        }

        public int AliSalvage
        {
            get;
            private set;
        }

        public int RoboSalvage
        {
            get;
            private set;
        }

        public Inventory(Character character)
        {
            this.character = character;
        }

        public void add(Item item)
        {
            HumSalvage++;
        }
    }
}
